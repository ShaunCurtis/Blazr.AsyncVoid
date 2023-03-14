namespace Blazr.AsyncVoid.Data;

public class ScopedService
{
    private ILogger<ScopedService> _logger;
    public Task LoadingTask = Task.CompletedTask;

    private List<string> _countries = new();

    public ScopedService(ILogger<ScopedService> logger)
    {
        _logger = logger;
        // start the async task and assign it to LoadingTask
        this.LoadingTask = this.LoadData()
            .Await(this.HandleSuccess, this.HandleFailure);
    }

    private void HandleFailure(Exception ex)
        => this._logger.LogCritical($"Log the error; {ex.Message}");

    private void HandleSuccess()
        => this._logger.LogInformation($"{this.GetType().Name} loaded successfully");

    public async Task LoadData()
        => await this.LoadCountries();

    public async Task<IEnumerable<string>> GetData()
    {
        // wait for the load task to complete before returning data
        await this.LoadingTask;
        return _countries;
    }

    private async Task LoadCountries()
    {
        await Task.Delay(1000);
        _countries.Clear();
        _countries.Add("UK");
        _countries.Add("France");
        _countries.Add("Portugal");
        _countries.Add("Spain");
        var x = Random.Shared.Next(1, 3);
        if (x == 2)
            throw new Exception("The number can't be 2!!!!");
    }
}
