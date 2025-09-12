window.initializeFirebase = function (config) {
    window.firebase = firebase.initializeApp(config);
    window.db = firebase.firestore();
};

 //Call this from Blazor with your Firebase config object
 //Javascript/firebase.js

