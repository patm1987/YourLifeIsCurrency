import * as functions from 'firebase-functions';
import * as admin from 'firebase-admin';

// Start writing Firebase Functions
// https://firebase.google.com/docs/functions/typescript

admin.initializeApp();

function generateGameCode(): string {
    const gameCodeCharacters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
    let gameCode = "";
    for (let i = 0; i < 4; i++) {
        gameCode += gameCodeCharacters.charAt(Math.floor(Math.random() * gameCodeCharacters.length));
    }
    return gameCode;
}

class GameResult{
    readonly success: boolean;
    readonly gameId: string;
    readonly reason: string;

    constructor(success: boolean, gameId: string, reason: string) {
        this.success = success;
        this.gameId = gameId;
        this.reason = reason;
    }

    static Success(gameId: string) {
        return new GameResult(true, gameId, "");
    }

    static Failure(reason: string) {
        return new GameResult(false, "", reason);
    }
}

export const createGame = functions.https.onCall((data, context) => {
    if (context.auth) {
        const gameId = generateGameCode();
        const db = admin.database();
        const gameRef = db.ref("games/" + gameId);
        const members: { [key: string]: boolean; } = {};
        members[context.auth.uid] = true;
        return gameRef.set({
            open: true,
            members: members,
            owner: context.auth.uid
        }).then(() => {
            return {
                success: true,
                game: gameId
            }
        }).catch((reason) => {
            console.log("Failed because " + reason);
            return {
                success: false,
                reason: reason
            };
        });
    }
    return {
        success: false,
        reason: "Not logged in"
    };
});

export const joinGame = functions.https.onCall((data, context) => {
    if (context.auth) {
        const gameId: string = data.text;
        const db = admin.database();
        const path = "games/" + gameId;
        const gameRef = db.ref(path);
        const auth = context.auth;
        return gameRef.once("value").then((dataSnapshot)=>{
            if (dataSnapshot.val() == null) {
                return GameResult.Failure("No data at path: " + path);
            }
            if (dataSnapshot.val().open) {
                const memberRef = db.ref("games/" + gameId + "/members/" + auth.uid);
                return memberRef.set(true).then(()=>{
                    return GameResult.Success(gameId);
                })
            }
            return GameResult.Failure("Game " + gameId + " is not open");
        })
    }
    return GameResult.Failure("Not logged in");
});

export const helloWorld = functions.https.onRequest((request, response) => {
    response.send("Hello from Firebase!");
});
