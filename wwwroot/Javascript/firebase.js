window.firebases = {
    firebaseConfig = {
        apiKey: "AIzaSyCuXJoMGNsbGubbWtKsBaCSt7OVwXrefgs",
        authDomain: "gospel-reach-tsa-cms.firebaseapp.com",
        projectId: "gospel-reach-tsa-cms",
        storageBucket: "gospel-reach-tsa-cms.firebasestorage.app",
        messagingSenderId: "810722788803",
        appId: "1:810722788803:web:ecdfa0cd2b116aa6c22976",
        measurementId: "G-CQ3ST5ZG3P"
    },


    // ✅ Make accessible to JS interop
    firebase = firebase.initializeApp(firebaseConfig); //Initialize firebase
    db = firebase.firestore()
}

