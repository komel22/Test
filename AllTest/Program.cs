using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
//Hello
class Program
{
    static async Task Main(string[] args)
    {
        string apiUrl = "https://mafiauniverse.org/api/Tournament/games?id=1576&page=1&pagesize=25";
        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<GameData>(json);

                    var playerList = data.Data.Data.Select(game => new
                    {
                        game_number = game.TourNumber,
                        player_1 = game.GamePlayers.FirstOrDefault(p => p.Position == 1)?.Player.Name,
                        player_2 = game.GamePlayers.FirstOrDefault(p => p.Position == 2)?.Player.Name,
                        player_3 = game.GamePlayers.FirstOrDefault(p => p.Position == 3)?.Player.Name,
                        player_4 = game.GamePlayers.FirstOrDefault(p => p.Position == 4)?.Player.Name,
                        player_5 = game.GamePlayers.FirstOrDefault(p => p.Position == 5)?.Player.Name,
                        player_6 = game.GamePlayers.FirstOrDefault(p => p.Position == 6)?.Player.Name,
                        player_7 = game.GamePlayers.FirstOrDefault(p => p.Position == 7)?.Player.Name,
                        player_8 = game.GamePlayers.FirstOrDefault(p => p.Position == 8)?.Player.Name,
                        player_9 = game.GamePlayers.FirstOrDefault(p => p.Position == 9)?.Player.Name,
                        player_10 = game.GamePlayers.FirstOrDefault(p => p.Position == 10)?.Player.Name,

                        // Добавьте аналогичные строки для других игроков
                    }).ToList();

                    File.WriteAllText("players.json", JsonConvert.SerializeObject(playerList));
                }
                else
                {
                    Console.WriteLine($"Ошибка: {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Исключение: {ex.Message}");
        }
    }
}

public class GameData
{
    public InnerData Data { get; set; }
    public bool Success { get; set; }

    public class InnerData
    {
        public Game[] Data { get; set; }
    }

    public class Game
    {
        public int TourNumber { get; set; }
        public int TableNumber { get; set; }
        public GamePlayer[] GamePlayers { get; set; }
    }

    public class GamePlayer
    {
        public int Position { get; set; }
        public Player Player { get; set; }
    }

    public class Player
    {
        public string Name { get; set; }
    }
}
