import { DutchGame, DutchScore, Token } from "./types.ts";

export const login = async (login: string, password: string): Promise<Token | null> => {
    try {
        const response = await fetch('http://localhost:5000/login', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            username: login,
            password: password,
          })
        });
    
        if(response.status === 401){
            console.log("nieprawdilowe dane logowania")
            return null;
        }

        if (!response.ok) {
          throw new Error(`Failed to login: ${response.statusText}`);
        }
    
        const token: Token = await response.json();
        
        return token;
      } catch (error) {
        console.error('Error when tried to log in:', error);
        return null;
      }
}

export const getScores = async (): Promise<DutchScore[]> => {
    try {
      const response = await fetch('http://localhost:5000/dutch/all', {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
          // If authentication is required, pass the JWT token like this:
          // 'Authorization': `Bearer ${yourToken}`
        },
      });
  
      if (!response.ok) {
        throw new Error(`Error fetching scores: ${response.statusText}`);
      }
  
      const data: DutchGame[] = await response.json();
      const scores = calculateDutchScores(data);
      
      return scores;
    } catch (error) {
      console.error('Error fetching Dutch scores:', error);
      return [];
    }
  };

  const calculateDutchScores = (games: DutchGame[]): DutchScore[] => {
    const groupedByNickName: { [key: string]: DutchGame[] } = games.reduce((acc, game) => {
        if (!acc[game.nickName]) {
            acc[game.nickName] = [];
        }
        acc[game.nickName].push(game);
        return acc;
    }, {} as { [key: string]: DutchGame[] });

    console.log(groupedByNickName)
    const dutchScores: DutchScore[] = Object.entries(groupedByNickName).map(([nickName, games]) => {
        const totalPoints = games.reduce((sum, game) => sum + game.points, 0);
        const totalPosition = games.reduce((sum, game) => sum + game.position, 0);
        const avgScore = totalPoints / games.length;
        const avgPosition = totalPosition / games.length;
        return {
            nickName,
            avgPosition: parseFloat(avgPosition.toFixed(1)),
            avgScore: parseFloat(avgScore.toFixed(1)),
            games: games.length
        };
    });

    const sortedDutchScores = dutchScores.sort((a, b) => a.avgScore - b.avgScore);

    return sortedDutchScores;
};