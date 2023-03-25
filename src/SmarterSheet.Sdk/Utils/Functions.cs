using Serilog;

namespace SmarterSheet.Sdk.Utils;

internal static class Functions
{
    /// <summary>
    /// Handles a HttpResponseMessage and tries to deserialize the content to the type of 'T'.
    /// </summary>
    /// <typeparam name="T">Type to be deserialized.</typeparam>
    /// <param name="response">HttpResponseMessage to be read.</param>
    /// <returns>Returns an object of type 'T' which is read from HttpResponseMessage</returns>
    public static async Task<T?> HandleResponseAsync<T>(this HttpResponseMessage response) where T : new()
    {
        if (!response.IsSuccessStatusCode)
        {
            Log.Error($"Http request failed: {response.StatusCode}");
            return default;
        }

        try
        {
            var content = await response.Content.ReadFromJsonAsync<T>();
            if (content == null)
                return default;

            return content;
        }
        catch
        {
            Log.Error($"Failed to deserialize response as {typeof(T)}: \n{await response.Content.ReadAsStringAsync()}");
        }

        return default;
    }

    /// <summary>
    /// Handles a HttpResponseMessage and tries to read a ResultObject with type of 'T'
    /// </summary>
    /// <typeparam name="T">Type to be deserialized</typeparam>
    /// <param name="response">HttpResponseMessage to be read.</param>
    /// <returns>Returns the deserialized object of 'T'</returns>
    public static async Task<T?> HandleResultResponseAsync<T>(this HttpResponseMessage response) where T : new()
    {
        if (!response.IsSuccessStatusCode)
        {
            Log.Error($"Http request failed: {await response.Content.ReadAsStringAsync()}");
            return default;
        }

        try
        {
            var content = await response.Content.ReadFromJsonAsync<ResultObject<T>>();
            if (content == null)
                return default;

            if (content.ResultCode != 0)
            {
                Log.Error(content.Message);
                return default;
            }

            return content.Result;
        }
        catch
        {
            Log.Error($"Failed to deserialize response as {typeof(T)}: \n{await response.Content.ReadAsStringAsync()}");
        }

        return default;
    }

    /// <summary>
    /// Handles a HttpResponseMessage and tries to read a ResultObject with type of 'T[]'
    /// </summary>
    /// <typeparam name="T">Type to be deserialized</typeparam>
    /// <param name="response">HttpResponseMessage to be read.</param>
    /// <returns>Returns the deserialized IEnumerable with type of 'T'.</returns>
    public static async Task<IEnumerable<T>> HandleResultsResponseAsync<T>(this HttpResponseMessage response) where T : new()
    {
        if (!response.IsSuccessStatusCode)
        {
            Log.Error($"Http request failed: {await response.Content.ReadAsStringAsync()}");
            return Array.Empty<T>();
        }

        try
        {
            var content = await response.Content.ReadFromJsonAsync<ResultObject<T[]>>();
            if (content == null)
                return Array.Empty<T>();

            if (content.ResultCode != 0)
            {
                Log.Error(content.Message);
                return Array.Empty<T>();
            }

            return content.Result;
        }
        catch
        {
            Log.Error($"Failed to deserialize response as {typeof(T)}: \n{await response.Content.ReadAsStringAsync()}");
        }

        return Array.Empty<T>();
    }

    /// <summary>
    /// Handles a HttpResponseMessage and checks if its successfull
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    public static bool HandleResponse(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            Log.Error($"Http request failed: {response.StatusCode}");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Handles a HttpResponseMessage that isn't supposed to return anything, it just checks if ResultCode isnt an error.
    /// </summary>
    /// <param name="response">HttpResponseMessage to be read.</param>
    /// <returns>Returns true if successfull.</returns>
    public static async Task<bool> HandleEmptyResultResponseAsync(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            Log.Error($"Http request failed: {response.StatusCode}");
            return false;
        }

        try
        {
            var content = await response.Content.ReadFromJsonAsync<EmptyResultObject>();

            if (content == null || content.ResultCode != 0)
                return false;

            return true;
        }
        catch
        {
            Log.Error($"Failed to deserialize response as EmptyResultResponse: \n{await response.Content.ReadAsStringAsync()}");
        }

        return false;
    }

    //TODO: Need a better way of handing paginated results
    /// <summary>
    /// Hadnles a HttpResponseMessage and tries to read a IndexResult with the type of 'T'
    /// </summary>
    /// <typeparam name="T">Type to be deserialized</typeparam>
    /// <param name="response">HttpResponseMessage to be read.</param>
    /// <returns>Returns the deserialized IEnumerable with type of 'T'.</returns>
    public static async Task<IEnumerable<T>> HandleIndexResultResponse<T>(this HttpResponseMessage response) where T : new()
    {
        if (!response.IsSuccessStatusCode)
        {
            Log.Error($"Http request failed: {response.StatusCode}");
            return Array.Empty<T>();
        }

        try
        {
            var content = await response.Content.ReadFromJsonAsync<IndexResult<T>>();
            if(content == null)
                return Array.Empty<T>();

            if (content.TotalCount <= 0)
            {
                Log.Error($"Result empty");
                return Array.Empty<T>();
            }

            return content.Data;
        }
        catch
        {
            Log.Error($"Failed to deserialize response as IndexResult<{typeof(T)}>: \n{await response.Content.ReadAsStringAsync()}");
        }

        return Array.Empty<T>();
    }

    public static CreateSheet ConvertToCreateSheet(this Sheet sheet)
    {
        if(sheet.Columns == null || !sheet.Columns.Any())
            return new CreateSheet { Name = sheet.Name };

        var createColumns = new CreateColumn[sheet.Columns.Length];

        for (var i = 0; i < sheet.Columns.Length; ++i)
        {
            createColumns[i] = new CreateColumn
            {
                Primary = sheet.Columns[i].Primary,
                Title = sheet.Columns[i].Title,
                Type = sheet.Columns[i].Type
            };
        }

        return new CreateSheet
        {
            Name = sheet.Name,
            Columns = createColumns
        };
    }
}