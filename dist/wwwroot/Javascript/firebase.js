window.initializeFirebase = function (config) {
    window.firebase = firebase.initializeApp(config);
    window.db = firebase.firestore();
}
