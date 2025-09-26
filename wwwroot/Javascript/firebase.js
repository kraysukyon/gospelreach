const firebaseConfig = {
    apiKey: "AIzaSyCuXJoMGNsbGubbWtKsBaCSt7OVwXrefgs",
    authDomain: "gospel-reach-tsa-cms.firebaseapp.com",
    projectId: "gospel-reach-tsa-cms",
    storageBucket: "gospel-reach-tsa-cms.firebasestorage.app",
    messagingSenderId: "810722788803",
    appId: "1:810722788803:web:ecdfa0cd2b116aa6c22976",
    measurementId: "G-CQ3ST5ZG3P"
};

// Initialize Firebase
const app = firebase.initializeApp(firebaseConfig);
const db = firebase.firestore();
const messaging = firebase.messaging();
window.myFirebase = { app, db }; // expose to Blazor
//window.initializeFirebase = function (config) {
//    window.firebase = firebase.initializeApp(config);
//    window.db = firebase.firestore();
//};

 //Call this from Blazor with your Firebase config object
 //Javascript/firebase.js

