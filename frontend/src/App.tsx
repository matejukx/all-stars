import './App.css'
import DutchScoresTable from './DuchScoreTable'
import { Header } from './Header'

function App() {
  return (
    <>
      <div id="container">
        <Header isLoggedIn={false}></Header>
        <DutchScoresTable></DutchScoresTable>
      </div>
    </>
  )
}

export default App
