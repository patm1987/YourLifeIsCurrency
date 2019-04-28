import * as functions from 'firebase-functions';
import * as admin from 'firebase-admin';

// Start writing Firebase Functions
// https://firebase.google.com/docs/functions/typescript

admin.initializeApp();

function generateGameCode(): string {
    const gameCodeCharacters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
    let gameCode = "";
    for(let i = 0; i < 4; i++) {
        gameCode += gameCodeCharacters.charAt(Math.floor(Math.random() * gameCodeCharacters.length));
    }
    return gameCode;
}

export const createGame = functions.https.onCall((data, context) => {
    if (context.auth) {
        let gameId = generateGameCode();
        const db = admin.database();
        const gameRef = db.ref("games/" + gameId);
        return gameRef.set({
            open: true,
            members: [context.auth.uid]
        }).then(() => {
            return {
                success: true,
                game: gameId
            }
        }).catch((reason)=>{
            console.log("Failed because " + reason);
            return {
                success: false,
                reason: reason
            }
        });
    }
    return {
        success: false,
        reason: "Not logged in"
    }
});

export const helloWorld = functions.https.onRequest((request, response) => {
    response.send("Hello from Firebase!");
});
