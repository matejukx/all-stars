import { useEffect, useState } from "react";
import { getScores } from "./api";
import { DutchScore } from "./types.ts";
import './dutchScoreTablesStyles.css'; 

  const DutchScoresTable = () => {

    const [scores, setScores] = useState<DutchScore[]>([]);

    useEffect(() => {
      const fetchScores = async () => {
        const data = await getScores();
        setScores(data);
      };
      fetchScores();
    }, []);

    return (
      <div className="container">
        <h1 className="heading">Dutch Scores</h1>
        <div className="tableWrapper">
          <table className="table">
            <thead className="tableHead">
              <tr>
                <th>Position</th>
                <th>Name</th>
                <th>Average Position</th>
                <th>Average Score</th>
                <th>Games</th>
              </tr>
            </thead>
            <tbody className="tableBody">
              {scores.map((score, index) => (
                <tr key={score.nickName}>
                  <td>{index + 1}</td>
                  <td>{score.nickName}</td>
                  <td>{score.avgPosition}</td>
                  <td>{score.avgScore.toFixed(1)}</td>
                  <td>{score.games}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    );
  };
  
  export default DutchScoresTable;