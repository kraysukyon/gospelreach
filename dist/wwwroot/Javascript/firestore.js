window.firestoreFunctions = {

    //============================================User Section============================================//

    //Get Accounts
    async getAccounts() {
        try {
            const snapshot = await db.collection("Users").get();
            return snapshot.docs.map(doc => ({ id: doc.id, ...doc.data() }));
        } catch (error) {
            alert(error)
        }
    },
    //Update Account
    async updateAccount(docId, newRole) {
        try {
            await db.collection("Users").doc(docId).update({
                role: newRole
            });
        } catch (error) {
            alert(error)
        }
    },

    //Disable Account
    async disableAccount(docId) {
        try {
            await db.collection("Users").doc(docId).update({ status: "Disabled" });
        } catch (error) {
            alert(error)
        }
    },
    //Enable Account
    async enableAccount(docId) {
        try {
            await db.collection("Users").doc(docId).update({ status: "Active" });
        } catch (error) {
            alert(error)
        }
    },

    //============================================Attendance Section============================================//

    //Getting Attendance
    async getAttendance() {
        try {
            const attendanceTable = await db.collection("Attendance").get();
            return attendanceTable.docs.map(items => ({ id: items.id, ...items.data() }));
        } catch (error) {
            alert(error);
        }
        
    },

    //Adding Attendance
    async addAttendance(attendance) {
        try {
            await db.collection("Attendance").add({
                date: attendance.date,
                category: attendance.category,
                activity: attendance.activity,
                count: attendance.count,
                seekers: attendance.seekers
            });
            return { success: true }
        } catch (error) {
            return { success: false }
        }
    },

    async editAttendance(docId, attendance) {
        try {
            await db.collection("Attendance").doc(docId).set(attendance);
        } catch (error) {
            alert(error)
        }
    },

    async deleteAttendance(docId) {
        try {
            await db.collection("Attendance").doc(docId).delete();
        } catch (error) {
            alert(error)
        }
    },


    //============================================Member Management Section============================================//
    async getMembers() {
        try {
            const membersTable = await db.collection("Members").get();
            return membersTable.docs.map(items => ({id: items.id, ...items.data()}));
        } catch (error) {
            alert(error)
        }
    },

    async addMember(member) {
        try {
            await db.collection("Members").add({
                firstName: member.firstName,
                middleName: member.middleName,
                lastName: member.lastName,
                email: member.email,
                contact: member.contact,
                birthdate: member.birthdate,
                dateOfSoldiership: member.dateOfSoldiership,
                classification: member.classification,
                status: member.status,
            });
            return { success: true }
        } catch (error) {
            return { success: false }
            alert(error)
        }
    },

    async updateMember(docId, member) {
        try {
            await db.collection("Members").doc(docId).set(member);
        } catch (error) {
            alert(error)
        }
    },

    async deleteMember(docId) {
        try {
            await db.collection("Members").doc(docId).delete();
        } catch (error) {
            alert(error)
        }
    },

    //============================================Event Management Section============================================//
    async getEvents() {
        try {
            const eventsTable = await db.collection("Events").get();
            return eventsTable.docs.map(items => ({ id: items.id, ...items.data() }));
        } catch (error) {
            alert(error)
        }
    },

    async addEvent(event) {
        try {
            await db.collection("Events").add({
                eventName: event.eventName,
                date: event.date,
                tag: event.tag,
                startTime: event.startTime,
                endTime: event.endTime,
                location: event.location,
                description: event.description
            });
            return { success: true }
        } catch (error) {
            return { success: false }
            alert(error)
        }
    },

    async editEvent(eventId, events) {
        try {
            await db.collection("Events").doc(eventId).set(events);
        } catch (error) {
            alert(error)
        }
    },

    async deleteEvent(eventId) {
        try {
            await db.collection("Events").doc(eventId).delete();
        } catch (error) {
            alert(error);
        }
    },

    //============================================Music Management Section============================================//
    async getSongs() {
        try {
            const songTable = await db.collection("Songs").get();
            return songTable.docs.map(items => ({ id: items.id, ...items.data() }));
        } catch (error) {
            alert(error)
        }
    },

    async addSong(song) {
        try {
            await db.collection("Songs").add({
                title: song.title,
                artist: song.artist,
                lyricsAndChords: song.lyricsAndChords
            });

            return { success: true };
        } catch (error) {
            return { success: false };
            alert(error);
        }
    },

    async updateSong(docId, song) {
        try {
            await db.collection("Songs").doc(docId).update({
                title: song.title,
                artist: song.artist,
                lyricsAndChords: song.lyricsAndChords
            });
        } catch (error) {
            alert(error)
        }
    },

    async deleteSong(id) {
        try {
            await db.collection("Songs").doc(id).delete();
        } catch (error) {
            alert(error);
        }
    },

    resizeTextarea(input, output) {
        if (!input || input.value.trim() === '') {
            input.style.height = "100px";
            output.style.height = "100px";
        }
        else {
            input.style.height = (input.scrollHeight) + "px";
            output.style.height = input.style.height;
        }
        
    }
    
}