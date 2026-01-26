import React, { useState, useEffect } from "react";
import axios from "axios";

const Games = () => {
  const [games, setGames] = useState([]);

  useEffect(() => {
    // Fetch games from an API or database
    const fetchGames = async () => {
      try {
        const response = await axios.get("http://localhost:5182/games");
        console.log(response);
        setGames(response.data);
      } catch (error) {
        console.error("Error fetching games:", error);
      }
    };
    fetchGames();
  }, []);

  return (
    <div>
      <h1>Game Listings</h1>
        <ul>
            {games.map((game) => (
                <li key={game.id}>
                    <h2>{game.name}</h2>
                    <p>Genre: {game.genre}</p>
                    <p>Price: ${game.price}</p>
                </li>
            ))}
        </ul>
    </div>
  );
};
export default Games;
