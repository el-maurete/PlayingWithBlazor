using System.Security.Cryptography.X509Certificates;
using Blazored.LocalStorage;

namespace Velocicapture;

public class Storage
{
    private readonly ISyncLocalStorageService _localStorageService;

    public Storage(ISyncLocalStorageService localStorageService) =>
      _localStorageService = localStorageService;

    public Team? LoadTeam()
    {
        if (_localStorageService.ContainKey("team"))
           return _localStorageService.GetItem<Team>("team");
        return null;
    }

    public void Store(Team? team)
    {
        if (team == null)
            _localStorageService.RemoveItem("team");
         else
            _localStorageService.SetItem("team", team);
    }

    public List<Sprint> LoadSprints()
    {
        if (_localStorageService.ContainKey("sprints"))
           return _localStorageService.GetItem<List<Sprint>>("sprints");
        return [];
    }

    public void Store(List<Sprint>? sprints)
    {
        if (sprints == null)
            _localStorageService.RemoveItem("sprints");
        _localStorageService.SetItem("sprints", sprints);
    }
}
