const firebaseConfig = {
    apiKey: "AIzaSyD8al1qptb_WKQGuhRvPOFvTRAZPg-J4uY",
    authDomain: "gospel-reach-cms.firebaseapp.com",
    projectId: "gospel-reach-cms",
    storageBucket: "gospel-reach-cms.firebasestorage.app",
    messagingSenderId: "199203547088",
    appId: "1:199203547088:web:dc1fc54cb156ddabf07bd5"
};


// ✅ Make accessible to JS interop
window.firebase = firebase.initializeApp(fireFunction.getKey); //Initialize firebase
window.db = firebase.firestore();