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
        public async Task<List<Song>> GetSongsAsync()
        {
            try
            {
                var result = await _js.InvokeAsync<Song[]>("firestoreFunctions.getSongs");
                return result.ToList();

            }
            catch (Exception ex)
            {
                await _js.InvokeVoidAsync("alert", ex.Message);
                return new List<Song>();
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
        public async Task UpdateSongAsync(string Id, Song song)
        {
            try
            {
                await _js.InvokeVoidAsync("firestoreFunctions.updateSong", Id, song);
            }
            catch (Exception ex)
            {
                await _js.InvokeVoidAsync("alert", ex.Message);
            }
        }

        public async Task DeleteSongAsync(string Id)
        {
            try
            {
                await _js.InvokeVoidAsync("firestoreFunctions.deleteSong", Id);
            }
            catch (Exception ex)
            {
                await _js.InvokeVoidAsync("alert", ex.Message);
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
