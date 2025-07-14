window.googleDriveFunctions = {
    tokenClient: null,
    accessToken: null,

    /**
     * Initialize the OAuth token client and request access if no valid token is stored.
     * @param {string} clientId - The OAuth 2.0 client ID from Google Cloud Console.
     * @param {string} scope - The OAuth scope, e.g., "https://www.googleapis.com/auth/drive.file".
     * @returns {Promise} Resolves when the user signs in or existing access token is restored.
     */
    init: async function (clientId, scope) {
        return new Promise((resolve, reject) => {
            // ✅ Check sessionStorage for stored token
            const storedToken = sessionStorage.getItem("google_drive_token");
            if (storedToken) {
                window.googleDriveFunctions.accessToken = storedToken;
                resolve(storedToken);
                return;
            }

            try {
                window.googleDriveFunctions.tokenClient = google.accounts.oauth2.initTokenClient({
                    client_id: clientId,
                    scope: scope,
                    ux_mode: 'popup',
                    callback: (tokenResponse) => {
                        if (tokenResponse && tokenResponse.access_token) {
                            window.googleDriveFunctions.accessToken = tokenResponse.access_token;
                            sessionStorage.setItem("google_drive_token", tokenResponse.access_token); // ✅ Save token to session
                            resolve(tokenResponse);
                        } else {
                            console.error("Token response invalid:", tokenResponse);
                            reject("Failed to get access token.");
                        }
                    }
                });

                // Launch popup to request access if no token
                window.googleDriveFunctions.tokenClient.requestAccessToken();
            } catch (err) {
                console.error("Error during Google Sign-In:", err);
                reject(err);
            }
        });
    },


    /**
     * Upload a file to Google Drive using a file input element.
     * @param {string} folderId - Google Drive folder ID to upload to.
     * @param {object} dotNetRef - Blazor .NET object reference for progress updates.
     */
    uploadFromInput: function (folderId, dotNetRef) {
        const input = document.getElementById("uploadInput");
        const file = input.files[0];
        if (!file) return;

        const metadata = {
            name: file.name,
            mimeType: file.type,
            parents: folderId ? [folderId] : []
        };

        const form = new FormData();
        form.append("metadata", new Blob([JSON.stringify(metadata)], { type: "application/json" }));
        form.append("file", file);

        const xhr = new XMLHttpRequest();
        xhr.open("POST", "https://www.googleapis.com/upload/drive/v3/files?uploadType=multipart", true);
        xhr.setRequestHeader("Authorization", "Bearer " + window.googleDriveFunctions.accessToken);

        xhr.upload.onprogress = function (e) {
            if (e.lengthComputable && dotNetRef) {
                const percent = Math.round((e.loaded / e.total) * 100);
                dotNetRef.invokeMethodAsync("ReportUploadProgress", percent);
            }
        };

        xhr.onload = function () {
            if (xhr.status >= 200 && xhr.status < 300) {
                dotNetRef.invokeMethodAsync("ReportUploadProgress", 100);
                dotNetRef.invokeMethodAsync("OnUploadCompleted");
            } else {
                alert("Upload failed: " + xhr.statusText);
            }
        };

        xhr.onerror = function () {
            alert("Upload error.");
        };

        xhr.send(form);
    },

    /**
     * List files in a Google Drive folder.
     * @param {string} folderId - Folder ID.
     * @returns {Promise<Array>} - List of file objects.
     */
    listFilesInFolder: async function (folderId) {
        if (!window.googleDriveFunctions.accessToken) {
            throw new Error("No access token. Please sign in.");
        }

        const query = `'${folderId}' in parents and trashed = false`;
        const fields = "files(id,name,size,webViewLink,thumbnailLink)";
        const url = `https://www.googleapis.com/drive/v3/files?q=${encodeURIComponent(query)}&fields=${fields}`;

        const response = await fetch(url, {
            headers: {
                Authorization: `Bearer ${window.googleDriveFunctions.accessToken}`
            }
        });

        const data = await response.json();
        return data.files || [];
    },

    /**
     * Delete a file from Drive.
     * @param {string} fileId - File ID.
     */
    deleteFile: async function (fileId) {
        const response = await fetch(`https://www.googleapis.com/drive/v3/files/${fileId}`, {
            method: "DELETE",
            headers: {
                Authorization: `Bearer ${window.googleDriveFunctions.accessToken}`
            }
        });

        if (!response.ok) throw new Error("Delete failed");
    },

    /**
     * Rename a file in Drive.
     * @param {string} fileId - File ID.
     * @param {string} newName - New file name.
     */
    renameFile: async function (fileId, newName) {
        const response = await fetch(`https://www.googleapis.com/drive/v3/files/${fileId}`, {
            method: "PATCH",
            headers: {
                Authorization: `Bearer ${window.googleDriveFunctions.accessToken}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ name: newName })
        });

        if (!response.ok) throw new Error("Rename failed");
    },

    /**
     * Download a file from Drive.
     * @param {string} fileId - File ID.
     * @param {string} fileName - Name for download.
     */
    downloadFile: async function (fileId, fileName) {
        const url = `https://www.googleapis.com/drive/v3/files/${fileId}?alt=media`;

        const response = await fetch(url, {
            method: "GET",
            headers: {
                "Authorization": "Bearer " + window.googleDriveFunctions.accessToken
            }
        });

        if (!response.ok) {
            throw new Error("Failed to download file: " + response.statusText);
        }

        const blob = await response.blob();
        const downloadUrl = window.URL.createObjectURL(blob);
        const a = document.createElement("a");
        a.href = downloadUrl;
        a.download = fileName;
        document.body.appendChild(a);
        a.click();
        a.remove();
        window.URL.revokeObjectURL(downloadUrl);
    },

    logout: function () {
        sessionStorage.removeItem("google_drive_token");
        window.googleDriveFunctions.accessToken = null;
    }
}