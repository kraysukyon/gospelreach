using GospelReachCapstone.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;

namespace GospelReachCapstone.Services
{
    public class MusicService
    {
        private readonly IJSRuntime _js;

        public MusicService(IJSRuntime js)
        {
            _js = js;
        }

        //======================================================Function===================================================//
        public async Task<SongResult> GetSongsAsync()
        {
            try
            {
                var result = await _js.InvokeAsync<SongResult>("firestoreFunctions.getSongs");
                return result;

            }
            catch (JSException ex)
            {
                return new SongResult { Success = false, Error = ex.Message };
            }
            catch (Exception ex)
            {
                return new SongResult { Success = false, Error = ex.Message };
            }
        }

        public async Task<SongResult> AddSongAsync(Song song)
        {
            try
            {
                var result = await _js.InvokeAsync<SongResult>("firestoreFunctions.addSong", song);
                return result;
            }
            catch (JSException ex)
            {
                return new SongResult { Success = false, Error = ex.Message };
            }
            catch (Exception ex)
            {
                return new SongResult { Success = false, Error = ex.Message };
            }
        }
        public async Task<SongResult> UpdateSongAsync(string Id, Song song)
        {
            try
            {
                var result = await _js.InvokeAsync<SongResult>("firestoreFunctions.updateSong", Id, song);
                return result;
            }
            catch (JSException ex)
            {
                return new SongResult { Success = false, Error = ex.Message };
            }
            catch (Exception ex)
            {
                return new SongResult { Success = false, Error = ex.Message };
            }
        }

        public async Task<SongResult> DeleteSongAsync(string Id)
        {
            try
            {
                var result = await _js.InvokeAsync<SongResult>("firestoreFunctions.deleteSong", Id);
                return result;
            }
            catch (JSException ex)
            {
                return new SongResult { Success = false, Error = ex.Message };
            }
            catch (Exception ex)
            {
                return new SongResult { Success = false, Error = ex.Message };
            }
        }

        //Get song by id
        public async Task<SongResult> GetSongById(string Id)
        {
            try
            {
                var result = await _js.InvokeAsync<SongResult>("firestoreFunctions.getSongById", Id);
                return result;
            }
            catch (JSException ex)
            {
                return new SongResult { Success = false, Error = ex.Message };
            }
            catch (Exception ex)
            {
                return new SongResult { Success = false, Error = ex.Message };
            }
        }

        //Resize textarea of input and output
        public async Task ResizeTextArea(ElementReference input, ElementReference output)
        {
            try
            {
                await _js.InvokeVoidAsync("firestoreFunctions.resizeTextarea", input, output);
            }
            catch (Exception ex)
            {
                await _js.InvokeVoidAsync("alert", ex.Message);
            }
        }
    }

    public class SongResult
    {
        public bool Success { get; set; }
        public List<Song> Data { get; set; }
        public string Error { get; set; }
        public Song Song { get; set; }
    }
}
