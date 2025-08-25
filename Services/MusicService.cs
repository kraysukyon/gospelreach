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

        public async Task<bool> AddSongAsync(Song song)
        {
            try
            {
                var result = await _js.InvokeAsync<JsonElement>("firestoreFunctions.addSong", song);
                return result.GetProperty("success").GetBoolean();
            }
            catch (Exception ex)
            {
                await _js.InvokeVoidAsync("alert", ex.Message);
                return false;
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
}
