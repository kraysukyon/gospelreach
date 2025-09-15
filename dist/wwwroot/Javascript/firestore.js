window.firestoreFunctions = {

    //============================================User Section============================================//

    //Get Accounts
    async getAccounts() {
        try {
            const docRef = await db.collection("Users").get();
            const doc = docRef.docs.map(u => ({ id: u.id, ...u.data() }));

            return { success: true, data: doc };
        } catch (error) {
            return { success: false, error: error.message }
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
            const attendance = attendanceTable.docs.map(items => ({ id: items.id, ...items.data() }));
            return { success: true, data: attendance };
        } catch (error) {
            return { success: false, error: error.message }
        }

    },

    // Get Attendance by month/year
    async getAttendanceByMonthYear(month, year) {
        try {
            // Convert to yyyy-MM-dd strings
            const jsMonth = month.toString().padStart(2, "0");
            const firstDay = `${year}-${jsMonth}-01`;
            const lastDate = new Date(year, month, 0).getDate(); // last day of month
            const lastDay = `${year}-${jsMonth}-${lastDate.toString().padStart(2, "0")}`;

            const attendanceTable = await db.collection("Attendance")
                .where("date", ">=", firstDay)
                .where("date", "<=", lastDay)
                .get();

            const attendance = attendanceTable.docs.map(items => ({
                id: items.id,
                ...items.data()
            }));

            return { success: true, data: attendance };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    //Adding Attendance
    async addAttendance(attendance) {
        try {
            const tb = await db.collection("Attendance").add({
                scheduleId: attendance.scheduleId,
                department: attendance.department,
                date: attendance.date,
                totalAttendee: attendance.totalAttendee,
                totalVisitors: attendance.totalVisitors,
                present: attendance.present,
                absent: attendance.absent,
                isCompleted: attendance.isCompleted,
            });
            return { success: true, id: tb.id }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    //Get attendance by Id
    async getAttendanceById(Id) {
        try {
            const docRef = db.collection("Attendance").doc(Id);
            const doc = await docRef.get();
            if (!doc.exists) {
                return { success: false, error: "Attendance not exist" };
            }
            else {
                const attdata = { id: doc.id, ...doc.data() };
                return { success: true, attendance: attdata };
            }

        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    //GetAttendance By scheduleId
    async getAttendanceByScheduleId(Id) {
        try {
            const docRef = await db.collection("Attendance").where("scheduleId", "==", Id).get();

            if (docRef.empty) {
                return { success: false, error: "Attendance not exist" };
            }
            else {
                const doc = docRef.docs.map(u => ({id:u.id, ...u.data()}));
                return { success: true, data: doc };
            }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    //update attendance
    async editAttendance(docId, attendance) {
        try {
            await db.collection("Attendance").doc(docId).update({
                scheduleId: attendance.scheduleId,
                department: attendance.department,
                date: attendance.date,
                totalAttendee: attendance.totalAttendee,
                totalVisitors: attendance.totalVisitors,
                present: attendance.present,
                absent: attendance.absent,
                isCompleted: attendance.isCompleted,
            });
            return { success: true }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    //delete attendance
    async deleteAttendance(docId) {
        try {
            await db.collection("Attendance").doc(docId).delete();
            return { success: true }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    //Get ongoing attendance
    async getOngoingAttendance() {
        try {
            // Get today's date in local yyyy-MM-dd
            const today = new Date();
            const year = today.getFullYear();
            const month = String(today.getMonth() + 1).padStart(2, "0");
            const day = String(today.getDate()).padStart(2, "0");
            const todayStr = `${year}-${month}-${day}`;

            // Query schedules where date >= today
            const schedTable = await db.collection("Attendance")
                .where("date", "==", todayStr)
                .get();

            const sched = schedTable.docs.map(u => ({ id: u.id, ...u.data() }));

            return { success: true, data: sched };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    //Get ongoing attendance
    async getOngoingAttendanceByDepartment(department) {
        try {
            // Get today's date in local yyyy-MM-dd
            const today = new Date();
            const year = today.getFullYear();
            const month = String(today.getMonth() + 1).padStart(2, "0");
            const day = String(today.getDate()).padStart(2, "0");
            const todayStr = `${year}-${month}-${day}`;

            // Query schedules where date >= today
            const schedTable = await db.collection("Attendance")
                .where("department", "==", department)
                .where("date", "==", todayStr)
                .get();

            if (!schedTable.empty) {
                const sched = schedTable.docs.map(u => ({ id: u.id, ...u.data() }));

                return { success: true, data: sched };
            }

            return { success: true, error: "No record found" };
            
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    async getCompletedAttendance() {
        try {
            // Get today's date in local yyyy-MM-dd
            const today = new Date();
            const year = today.getFullYear();
            const month = String(today.getMonth() + 1).padStart(2, "0");
            const day = String(today.getDate()).padStart(2, "0");
            const todayStr = `${year}-${month}-${day}`;

            // Query schedules where date >= today
            const schedTable = await db.collection("Attendance")
                .where("date", "<", todayStr)
                .get();

            const sched = schedTable.docs.map(u => ({ id: u.id, ...u.data() }));

            return { success: true, data: sched };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    async getCompletedAttendanceByDepartment(department) {
        try {
            // Get today's date in local yyyy-MM-dd
            const today = new Date();
            const year = today.getFullYear();
            const month = String(today.getMonth() + 1).padStart(2, "0");
            const day = String(today.getDate()).padStart(2, "0");
            const todayStr = `${year}-${month}-${day}`;

            // Query schedules where date >= today
            const schedTable = await db.collection("Attendance")
                .where("department", "==", department)
                .where("date", "<", todayStr)
                .get();

            if (!schedTable.empty) {
                const sched = schedTable.docs.map(u => ({ id: u.id, ...u.data() }));

                return { success: true, data: sched };
            }

            return { success: true, error: "No record found" };

            return { success: true, data: sched };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    // delete attendance by scheduleId (unique)
    async removeAttendanceByScheduleId(docId) {
        try {
            const snapshot = await db.collection("Attendance")
                .where("scheduleId", "==", docId)
                .get();

            if (snapshot.empty) {
                return { success: false, error: "No attendance record found" };
            }

            // get the first (and only) document
            const docRef = snapshot.docs[0].ref;

            // delete it
            await docRef.delete();
            return { success: true };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    async updateAttendanceStatus(Id, isComplete, visitor, present, absent) {
        try {
            const docRef = db.collection("Attendance").doc(Id);
            const doc = await docRef.get();

            if (!doc.exists) {
                return { success: false, error: "Attendance not found" };
            }
            else {
                await docRef.update({
                    isCompleted: isComplete,
                    totalVisitors: visitor,
                    present: present,
                    absent: absent,
                });
                return { success: true };
            }

        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    async getAttendanceByDateRange(date1, date2) {
        try {
            // Expecting date1 and date2 as "yyyy-MM-dd" strings
            const docRef = await db.collection("Attendance")
                .where("isCompleted", "==", true)
                .where("date", ">=", date2)
                .where("date", "<=", date1)
                .get();

            const doc = docRef.docs.map(items => ({
                id: items.id,
                ...items.data()
            }));

            return { success: true, data: doc };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    async getDepartmentAttendanceByDateRange(date1, date2, department) {
        try {
            // Expecting date1 and date2 as "yyyy-MM-dd" strings
            const docRef = await db.collection("Attendance")
                .where("department", "==", department)
                .where("isCompleted", "==", true)
                .where("date", ">=", date2)
                .where("date", "<=", date1)
                .get();

            const doc = docRef.docs.map(items => ({
                id: items.id,
                ...items.data()
            }));

            return { success: true, data: doc };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    //============================================Attendance Member Record=============================================
    async addAttendanceMemberRecor(att) {
        try {
            await db.collection("AttendanceMemberRecord").add({
                attendanceId: att.attendanceId,
                memberId: att.memberId,
                isPresent: att.isPresent
            });

            return { success: true }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    async removeAttendanceMember(id) {
        try {
            const snapshot = await db.collection("AttendanceMemberRecord").where("attendanceId", "==", id).get();
            const batch = db.batch();

            snapshot.forEach(doc => { batch.delete(doc.ref) });

            await batch.commit();

            return { success: true }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    async getAttendanceMemberByAttendanceId(attId) {
        try {
            const attMemberTable = await db.collection("AttendanceMemberRecord").where("attendanceId", "==", attId).get();
            const attMember = attMemberTable.docs.map(items => ({ id: items.id, ...items.data() }));

            return { success: true, data: attMember }
        } catch (error) {
            return { success: false, error: error.message }
        }

    },


    async updateMemberAttendance(Id, memberId, attend) {
        try {
            const docRef = await db.collection("AttendanceMemberRecord").where("attendanceId", "==", Id).where("memberId", "==", memberId).get();

            if (docRef.empty) {
                return { success: false, error: "Data not exist" }
            }
            else {
                const query = docRef.docs[0].ref;
                await query.update({
                    isPresent: attend
                });

                return { success: true }
            }
            
            
        } catch (error) {
            return { success: false, error: error.message }
        }

    },


    //============================================Attendance Visitor=============================================
    async addAttendanceVisitorRecord(att) {
        try {
            await db.collection("AttendanceVisitorRecord").add({
                attendanceId: att.attendanceId,
                visitorId: att.visitorId,
                invitedByMemberId: att.invitedByMemberId,
                isPresent: att.isPresent
            });

            return { success: true }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    async removeAttendanceVisitor(id) {
        try {
            const snapshot = await db.collection("AttendanceVisitorRecord").where("attendanceId", "==", id).get();
            const batch = db.batch();

            snapshot.forEach(doc => { batch.delete(doc.ref) });

            await batch.commit();

            return { success: true }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    async getAttendanceVisitorByAttendanceId(attId) {
        try {
            const attMemberTable = await db.collection("AttendanceVisitorRecord").where("attendanceId", "==", attId).get();
            const attMember = attMemberTable.docs.map(items => ({ id: items.id, ...items.data() }));

            return { success: true, data: attMember }
        } catch (error) {
            return { success: false, error: error.message }
        }

    },


    //============================================Member Management Section============================================//
    //fetch member list
    async getMembers() {
        try {
            const membersTable = await db.collection("Members").get();
            const member = membersTable.docs.map(items => ({ id: items.id, ...items.data() }));

            return { success: true, data: member }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    //add member
    async addMember(member) {
        try {
            await db.collection("Members").add({
                firstName: member.firstName,
                middleName: member.middleName,
                lastName: member.lastName,
                gender: member.gender,
                email: member.email,
                contact: member.contact,
                birthdate: member.birthdate,
                age: member.age,
                classification: member.classification,
                status: member.status,
            });
            return { success: true }
        } catch (error) {
            return { success: false }
            alert(error)
        }
    },

    //update memeber
    async updateMember(docId, member) {
        try {
            await db.collection("Members").doc(docId).set(member);
        } catch (error) {
            alert(error)
        }
    },

    //remove member
    async deleteMember(docId) {
        try {
            await db.collection("Members").doc(docId).delete();
        } catch (error) {
            alert(error)
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
        input.style.height = 'auto';
        if (!input || input.value.trim() === '') {
            //input.style.height = "100px";
            //output.style.height = "100px";
            input.style.height = (input.scrollHeight) + "px";
        }
        else {
            input.style.height = (input.scrollHeight) + "px";
            output.style.height = input.style.height;
        }

    },

    //============================================Department Management Section============================================//
    async getDepartment() {
        try {
            const departmentTable = await db.collection("Departments").get();
            const departments = departmentTable.docs.map(items => ({ departmentId: items.id, ...items.data() }));

            return { success: true, data: departments };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    async addDepartment(department) {
        try {
            await db.collection("Departments").add({
                departmentName: department.departmentName,
            });

            return { success: true }
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    async getDepartmentById(id) {
        try {
            const docRef = db.collection("Departments").doc(id);
            const doc = await docRef.get();

            if (doc.exists) {
                return { success: true, data: { id: doc.id, ...doc.data() } };
            }

            return { success: false, error: "Department not found" };
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    async updateDepartment(id, department) {
        try {
            await db.collection("Departments").doc(id).update({
                departmentName: department.departmentName
            });

            return { success: true }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    async removeDepartment(id) {
        try {
            await db.collection("Departments").doc(id).delete();

            return { success: true }
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    //============================================Sub Department Section=======================================================//
    async getSubDepartment(id) {
        try {
            const subtable = await db.collection("SubDepartment").where("departmentId", "==", id).get();
            const sub = subtable.docs.map(u => ({ id: u.id, ...u.data() }));

            return { success: true, data: sub }

        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    //============================================DepartmentMember Management Section============================================//

    async getDepartmentMembers() {
        try {
            //const membersTable = await db.collection("DepartmentMembers").where("departmentId", "==", depId).get();
            const membersTable = await db.collection("DepartmentMembers").get();
            const members = membersTable.docs.map(item => ({ DepartmentMemberId: item.id, ...item.data() }));

            return { success: true, data: members };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    async addDepartmentMember(member) {
        try {
            await db.collection("DepartmentMembers").add({
                departmentId: member.departmentId,
                memberId: member.memberId
            });

            return { success: true };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    async updateDepartmmentMember(id, member) {
        try {
            await db.collection("DepartmentMembers").doc(id).set(member);
            return { success: true }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    async removeDepartmentmember(id) {
        try {
            await db.collection("DepartmentMembers").doc(id).delete();
            return { success: true }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    //============================================Schedule Management Section============================================//

    async addSchedule(schedule) {
        try {
            const docRef = await db.collection("Schedules").add({
                title: schedule.title,
                categoryId: schedule.categoryId,
                departmentId: schedule.departmentId,
                divisionId: schedule.divisionId,
                startDate: schedule.startDate,
                endDate: schedule.endDate,
                startTime: schedule.startTime,
                endTime: schedule.endTime,
                timeOption: schedule.timeOption,
                location: schedule.location,
                description: schedule.description,
                hasAttendee: schedule.hasAttendee,
                attendeeType: schedule.attendeeType,
                groupId: schedule.groupId,
                status: schedule.status,
                hasAttendance: schedule.hasAttendance,
                hasFinance: schedule.hasFinance,
            });
           
            return { success: true, id: docRef.id}
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    async getScheduleByDate(month, year) {
        try {
            // Convert to yyyy-MM-dd strings
            const jsMonth = month.toString().padStart(2, "0");
            const firstDay = `${year}-${jsMonth}-01`;
            const lastDate = new Date(year, month, 0).getDate(); // last day of month
            const lastDay = `${year}-${jsMonth}-${lastDate.toString().padStart(2, "0")}`;

            const schedTable = await db.collection("Schedules")
                .where("startDate", ">=", firstDay)
                .where("startDate", "<=", lastDay)
                .get();

            const sched = schedTable.docs.map(items => ({
                id: items.id,
                ...items.data()
            }));

            return { success: true, data: sched };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    async getScheduleByDateRange(date1, date2) {
        try {
            // Expecting date1 and date2 as "yyyy-MM-dd" strings
            const financeMensTable = await db.collection("FinanceRecord")
                .where("date", ">=", date2)
                .where("date", "<=", date1)
                .get();

            const finance = financeMensTable.docs.map(items => ({
                id: items.id,
                ...items.data()
            }));

            return { success: true, data: finance };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    async getSchedule() {
        try {
            const schedTable = await db.collection("Schedules").get();
            const sched = schedTable.docs.map(u => ({ id: u.id, ...u.data() }));

            return { success: true, data: sched }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    async getScheduleByListOfIds(idList) {
        try {
            if (!idList || idList.length === 0) {
                return { success: true, data: [] }; // nothing to fetch
            }

            // Firestore 'in' supports max 30 IDs per query
            // Split into batches if needed
            const batchSize = 30;
            let schedules = [];

            for (let i = 0; i < idList.length; i += batchSize) {
                const batchIds = idList.slice(i, i + batchSize);

                const schedSnap = await db.collection("Schedules")
                    .where(firebase.firestore.FieldPath.documentId(), "in", batchIds)
                    .get();

                const batchSched = schedSnap.docs.map(doc => ({
                    id: doc.id,
                    ...doc.data()
                }));

                schedules = schedules.concat(batchSched);
            }

            return { success: true, data: schedules };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    async getScheduleById(schedId) {
        try {
            const docRef = db.collection("Schedules").doc(schedId);
            const doc = await docRef.get();

            if (!doc.exists) {
                //console.log("No document found for ID:", schedId); // 👈 log missing doc
                return { success: false, error: "Event does not exist!" }
            }
            else {
                const scheduleData = { id: doc.id, ...doc.data() };
                //console.log("Document found:", scheduleData); // 👈 log doc data
                return { success: true, Schedule: scheduleData };
            }
            
        } catch (error) {
            console.error("Error fetching schedule:", error); // 👈 log error
            return { success: false, error: error.message }
        }
    },

    async getUpcomingSchedules() {
        try {
            // Get today's date in local yyyy-MM-dd
            const today = new Date();
            const year = today.getFullYear();
            const month = String(today.getMonth() + 1).padStart(2, "0");
            const day = String(today.getDate()).padStart(2, "0");
            const todayStr = `${year}-${month}-${day}`;

            // Query schedules where date >= today
            const schedTable = await db.collection("Schedules")
                .where("startDate", ">", todayStr)
                .get();

            const sched = schedTable.docs.map(u => ({ id: u.id, ...u.data() }));

            return { success: true, data: sched };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    async getOngoingSchedules() {
        try {
            // Get today's date in local yyyy-MM-dd
            const today = new Date();
            const year = today.getFullYear();
            const month = String(today.getMonth() + 1).padStart(2, "0");
            const day = String(today.getDate()).padStart(2, "0");
            const todayStr = `${year}-${month}-${day}`;

            // Query schedules where date >= today
            const schedTable = await db.collection("Schedules")
                .where("startDate", "==", todayStr)
                .get();

            const sched = schedTable.docs.map(u => ({ id: u.id, ...u.data() }));

            return { success: true, data: sched };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    async getCompletedSchedules() {
        try {
            // Get today's date in local yyyy-MM-dd
            const today = new Date();
            const year = today.getFullYear();
            const month = String(today.getMonth() + 1).padStart(2, "0");
            const day = String(today.getDate()).padStart(2, "0");
            const todayStr = `${year}-${month}-${day}`;

            // Query schedules where date >= today
            const schedTable = await db.collection("Schedules")
                .where("startDate", "<", todayStr)
                .get();

            const sched = schedTable.docs.map(u => ({ id: u.id, ...u.data() }));

            return { success: true, data: sched };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    async getSchedulesWithMissingFinancialRecords(department) {
        try {
            // Get today's date in local yyyy-MM-dd
            const today = new Date();
            const year = today.getFullYear();
            const month = String(today.getMonth() + 1).padStart(2, "0");
            const day = String(today.getDate()).padStart(2, "0");
            const todayStr = `${year}-${month}-${day}`;

            // Query schedules where date >= today

            const schedTable = await db.collection("Schedules")
                .where("departmentId", "==", department)
                .where("startDate", "<=", todayStr)
                .where("hasFinance", "==", false)
                .get();

            const sched = schedTable.docs.map(u => ({ id: u.id, ...u.data() }));

            return { success: true, data: sched };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    async updateScheduleAttendance(Id, att) {
        try {
            await db.collection("Schedules").doc(Id).update({
                hasAttendance: att
            });

            return { success: true }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    async updateSchedule(id, schedule) {
        try {
            await db.collection("Schedules").doc(id).update({
                title: schedule.title,
                categoryId: schedule.categoryId,
                departmentId: schedule.departmentId,
                divisionId: schedule.divisionId,
                startDate: schedule.startDate,
                endDate: schedule.endDate,
                startTime: schedule.startTime,
                endTime: schedule.endTime,
                timeOption: schedule.timeOption,
                location: schedule.location,
                description: schedule.description,
                hasAttendee: schedule.hasAttendee,
                attendeeType: schedule.attendeeType,
                groupId: schedule.groupId,
                status: schedule.status
            });
            return { success: true }
        } catch (error) {
            return { success: true, error: error.message }
        }
    },

    async removeSchedule(Id) {
        try {
            await db.collection("Schedules").doc(Id).delete();
            return { success: true }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    async updateScheduleFinance(Id, finance) {
        try {
            await db.collection("Schedules").doc(Id).update({
                hasFinance: finance,
            });

            return {success: true }
        } catch (error) {
            return {success: false, error: error.message}
        }
    },

    async getSubCategory() {
        try {
            const subCatTable = await db.collection("SubCategories").get();
            const subCat = subCatTable.docs.map(u => ({ id: u.id, ...u.data() }));

            return { success: true, data: subCat }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    textareaResize(input) {
        input.style.height = 'auto';
        //input.style.height = this.scrollHeight + 'px';

        if (!input || input.value.trim() === '') {
            //input.style.height = 'calc(1.5em + .75rem + 2px)';
            input.style.height = (input.scrollHeight) + "px";
        }
        else {
            input.style.height = (input.scrollHeight) + "px";
        }
    },

    //============================================Group Section============================================//

    async getGroup() {
        try {
            const groupTable = await db.collection("Groups").get();
            const group = groupTable.docs.map(u => ({ id: u.id, ...u.data() }));

            return { success: true, data: group }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    async addGroup(group) {
        try {
            const docref = await db.collection("Groups").add({
                name: group.name,
            });

            return { success: true, Id: docref.id }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    async getGroupById(id) {
        try {
            const docRef = db.collection("Groups").doc(id);
            const doc = await docRef.get();

            if (!doc.exists) {
                return { success: false, error: "Group not found" }
            }

            return { success: true, group: { id: doc.id, ...doc.data() } }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    async updateGroup(group) {
        try {
            await db.collection("Groups").doc(group.id).update({
                name: group.name
            });

            return { success: true }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    async removeGroup(id) {
        try {
            const groupTable = db.collection("Groups").doc(id);
            const group = await groupTable.get();

            if (group.exists) {
                const snapshot = await db.collection("GroupMembers").where("groupId", "==", id).get();
                const batch = db.batch();

                snapshot.forEach(doc => { batch.delete(doc.ref) });

                await batch.commit();

                await db.collection("Groups").doc(id).delete();


                return { success: true }
            }

            return { success: false, error: "Group not found" };
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    async renameGroup(Id, groupName) {
        try {
            const groupTable = db.collection("Groups").doc(Id);
            const group = await groupTable.get();

            if (group.exists) {
                await db.collection("Groups").doc(Id).update({
                    name: groupName
                });

                return { success: true }
            }

            return { success: false, error: "Group does not exist" }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    //============================================GroupMember Section============================================//
    async getGroupMembersByGroupId(id) {
        try {
            const groupMembertable = await db.collection("GroupMembers").where("groupId", "==", id).get();

            if (groupMembertable.empty) {
                return { success: true, error: "No members found for this group" }
            }

            const groupMember = groupMembertable.docs.map(u => ({ id: u.id, ...u.data() }));

            return { success: true, data: groupMember }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },


    async addGroupmembers(mem) {
        try {
            await db.collection("GroupMembers").add({
                groupId: mem.groupId,
                memberId: mem.memberId
            });

            return { success: true }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    async removeGroupMembers(id) {
        try {
            const snapshot = await db.collection("GroupMembers").where("memberId", "==", id).get();
            const batch = db.batch();

            snapshot.forEach(doc => { batch.delete(doc.ref) });

            await batch.commit();

            return { success: true }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },




    //==========================================Visitor Management Section=================================================

    async getVisitors() {
        try {
            const visitorTable = await db.collection("Visitors").get();
            const visitors = visitorTable.docs.map(u => ({ id: u.id, ...u.data() }));

            return { success: true, data: visitors }

        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    async addVisitor(visitor) {
        try {
            const vis = await db.collection("Visitors").add({
                firstName: visitor.firstName,
                middleName: visitor.middleName,
                lastName: visitor.lastName,
                invitedByMemberId: visitor.invitedByMemberId,
                firstVisitDate: visitor.firstVisitDate,
            });

            return { success: true, id: vis.id }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },


    //==========================================Finance Section=================================================
    async addFinancialRecord(rec) {
        try {
            await db.collection("FinanceRecord").add({
                department: rec.department,
                type: rec.type,
                category: rec.category,
                amount: rec.amount,
                scheduleId: rec.scheduleId,
                scheduleTitle: rec.scheduleTitle,
                donatorName: rec.donatorName,
                invoiceNumber: rec.invoiceNumber,
                voucherNumber: rec.voucherNumber,
                date: rec.date,
                dateAdded: rec.dateAdded,
                addedByUserId: rec.addedByUserId,
            });

            return { success: true }
        } catch (error) {
            return { success: true, error: error.message }
        }
    },

    async getAllFinance() {
        try {
            const docRef = await db.collection("FinanceRecord").get();

            if (!docRef.empty) {
                const doc = docRef.docs.map(u => ({ id: u.id, ...u.data() }));
                return { success: true, data: doc };
            }

            return { success: true, error: "No records found" };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    async getAllFinanceByDepartment(department) {
        try {
            const docRef = await db.collection("FinanceRecord").where("department", "==", department).get();

            if (docRef.empty) {
                return { success: true, error: "No records found" };
            }
            else {
                const doc = docRef.docs.map(u => ({ id: u.id, ...u.data() }));
                return { success: true, data: doc };
            }
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    async getFinanceMensById(Id) {
        try {
            const docRef = db.collection("FinanceRecord").doc(Id);
            const doc = await docRef.get();

            if (!doc.exists) {
                return { success: true, error: "Record does not exist" };
            }
            else {
                const data = {id: doc.id, ...doc.data()}
                return { success: true, FinanceMens: data };
            }

        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    async removeFinanceMens(Id) {
        try {
            const docRef = db.collection("FinanceRecord").doc(Id);
            const doc = await docRef.get();

            if (!doc.exists) {
                return { success: false, error: "Record not found" }
            }

            await docRef.delete();
            return { success: true };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    async getFinancialRecordByDateRange(date1, date2) {
        try {
            // Expecting date1 and date2 as "yyyy-MM-dd" strings
            const financeMensTable = await db.collection("FinanceRecord")
                .where("date", ">=", date2)
                .where("date", "<=", date1)
                .get();

            const finance = financeMensTable.docs.map(items => ({
                id: items.id,
                ...items.data()
            }));

            return { success: true, data: finance };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    async getDepartmentFinancialRecordByDateRange(date1, date2, department) {
        try {
            // Expecting date1 and date2 as "yyyy-MM-dd" strings
            const financeMensTable = await db.collection("FinanceRecord")
                .where("department", "==", department)
                .where("date", ">=", date2)
                .where("date", "<=", date1)
                .get();

            const finance = financeMensTable.docs.map(items => ({
                id: items.id,
                ...items.data()
            }));

            return { success: true, data: finance };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    async checkReceiptNumber(invoice) {
        try {
            // 1. Check invoice numbers
            const invoiceSnap = await db
                .collection("FinanceRecord")
                .where("invoiceNumber", "==", invoice)
                .get();

            if (!invoiceSnap.empty) {
                return { success: true, error: "-Invoice number already exists." };
            }

            // 2. Check voucher numbers
            const voucherSnap = await db
                .collection("FinanceRecord")
                .where("voucherNumber", "==", invoice)
                .get();

            if (!voucherSnap.empty) {
                return { success: true, error: "-Voucher number already exists." };
            }

            // If neither exists
            return { success: true };

        } catch (error) {
            return { success: false, error: "Error checking number: " + error.message };
        }
    },


    async updateFinanceMensRecord(Id, rec) {
        try {
            const docRef = db.collection("FinanceRecord").doc(Id);
            const doc = await docRef.get();

            if (!doc.exists) {
                return { success: true, error: "Record does not exist" };
            }
            else {
                await docRef.update({
                    department: rec.department,
                    type: rec.type,
                    category: rec.category,
                    amount: rec.amount,
                    scheduleId: rec.scheduleId,
                    scheduleTitle: rec.scheduleTitle,
                    donatorName: rec.donatorName,
                    invoiceNumber: rec.invoiceNumber,
                    voucherNumber: rec.voucherNumber,
                    date: rec.date,
                    dateModified: rec.dateModified,
                    modifiedByUserId: rec.modifiedByUserId
                });

                return { success: true };
            }

            
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    

    async renderIncomeBreakdown(offering, donation) {
        const ctx = document.getElementById('incomeBreakdown').getContext('2d');
        new Chart(ctx, {
            type: 'doughnut',
            data: {
                labels: ['Offering', 'Donation'],
                datasets: [{
                    data: [offering, donation],
                    backgroundColor: ['#f28749', '#4473ca'],
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: { position: 'bottom' },
                    datalabels: {
                        color: '#fff',
                        font: { weight: 'bold', size: 14 },
                        formatter: (value, ctx) => {
                            // Show both value and percentage
                            let sum = ctx.chart.data.datasets[0].data.reduce((a, b) => a + b, 0);
                            let percentage = (value * 100 / sum).toFixed(1) + "%";
                            return value + " (" + percentage + ")";
                        }
                    }
                }
            },
            plugins: [ChartDataLabels] // ✅ enable datalabels plugin
        });
    },

    //==========================================Finance Events Section=================================================

    async addFinanceEvent(event) {
        try {
            await db.collection("FinanceEvents").add({
                departmentId: event.departmentId,
                scheduleId: event.scheduleId,
                date: event.date,
                isCompleted: event.isCompleted
            });

            return { success: true }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    async getAllFinanceEvents() {
        try {
            const docRef = await db.collection("FinanceEvents").get();

            if (!docRef.empty) {
                const doc = docRef.docs.map(u => ({ id: u.id, ...u.data() }));
                return { success: true , data: doc}
            }

            return { success: true, error: "No data found" };
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    //Remove finance records that contains schedule id
    async removeFinanceEventsByScheduleId(schedId) {
        try {
            // 1. Query all docs with the matching scheduleId
            const querySnap = await db.collection("FinanceEvents")
                .where("scheduleId", "==", schedId)
                .get();

            if (querySnap.empty) {
                // no documents found
                return { success: false, error: "No records found" };
            }

            // 2. Delete each doc
            const batch = db.batch(); // use batch to delete all at once

            querySnap.forEach(doc => {
                batch.delete(doc.ref);
            });

            await batch.commit();

            return { success: true };

        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    //Get documesnt that match department and schedule id
    async getFinanceEventsByDepartment(dept) {
        try {
            const docRef = await db.collection("FinanceEvents")
                .where("departmentId", "==", dept)
                .where("isCompleted", "==", false).get();

            if (docRef.empty) {
                return { success: true, error: "No records found" }
            }

            const doc = docRef.docs.map(u => ({ id: u.id, ...u.data() }));
            return { success: true, data: doc }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    async updateFinanceEvent(id, isComplete) {
        try {
            await db.collection("FinanceEvents").doc(id).update({
                isCompleted: isComplete
            });

            return { success: true };
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    //============================================Chart Section============================================//

    async renderExpenseBreakdown(tithes, property, transportation, incidental, others ) {
        const ctx = document.getElementById('expenseBreakdown').getContext('2d');
        new Chart(ctx, {
            type: 'doughnut',
            data: {
                labels: ['Tithes', 'Property Maintenance', 'Transportation', 'Incidental', 'Others'],
                datasets: [{
                    data: [tithes, property, transportation, incidental, others],
                    backgroundColor: [
                        '#f28749', // orange
                        '#4473ca', // blue
                        '#64D223', // green
                        '#C6D223', // yellow
                        '#D22323'  // red
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        position: 'bottom'
                    },
                    datalabels: {
                        color: '#fff',
                        font: { weight: 'bold', size: 13 },
                        formatter: (value, ctx) => {
                            // calculate total
                            let sum = ctx.chart.data.datasets[0].data.reduce((a, b) => a + b, 0);
                            let percentage = ((value / sum) * 100).toFixed(1) + "%";
                            // show value + percentage
                            return value + " (" + percentage + ")";
                        }
                    }
                }
            },
            plugins: [ChartDataLabels] // ✅ enable the datalabels plugin
        });
    },

    async renderIncomeExpenseLineChart(months, incomeData, expenseData) {
        const ctx = document.getElementById('incomeExpenseChart').getContext('2d');
        new Chart(ctx, {
            type: 'line',
            data: {
                labels: months, // e.g., ["Jan", "Feb", "Mar", ...]
                datasets: [
                    {
                        label: 'Income',
                        data: incomeData, // e.g., [5000, 7000, 8000, ...]
                        borderColor: '#28a745',
                        backgroundColor: 'rgba(40,167,69,0.2)',
                        fill: true,
                        tension: 0.3, // smooth curve
                        borderWidth: 2,
                        pointRadius: 4,
                        pointBackgroundColor: '#28a745'
                    },
                    {
                        label: 'Expense',
                        data: expenseData, // e.g., [3000, 4000, 6000, ...]
                        borderColor: '#dc3545',
                        backgroundColor: 'rgba(220,53,69,0.2)',
                        fill: true,
                        tension: 0.3,
                        borderWidth: 2,
                        pointRadius: 4,
                        pointBackgroundColor: '#dc3545'
                    }
                ]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        position: 'top'
                    },
                    tooltip: {
                        mode: 'index',
                        intersect: false
                    }
                },
                interaction: {
                    mode: 'nearest',
                    axis: 'x',
                    intersect: false
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        title: {
                            display: true,
                            text: 'Amount (₱)'
                        }
                    },
                    x: {
                        title: {
                            display: true,
                            text: 'Month (2025)'
                        }
                    }
                }
            }
        });
    },
}