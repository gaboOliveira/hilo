import { useState, KeyboardEvent, MouseEvent, useEffect } from 'react';
import { Wrapper, Input, Title, RequestNameMessage, StartButton } from './App.styles';
import { IAttempt, IGame, IUser } from './Models';

function App() {
  const apiBaseURL = "https://localhost:7111/api";

  const [user, setUser] = useState<IUser>();
  const [userName, setUserName] = useState<string>('');
  const [game, setGame] = useState<IGame>();
  const [misteryNumber, setMisteryNumber] = useState<number | null>(null);
  const [attempt, setAttempt] = useState<IAttempt>();
  
  const handleKeyDown = (event: KeyboardEvent<HTMLInputElement>) => {
    if (event.key === "Enter"){
      if (!user && userName) {
        fetch(`${apiBaseURL}/player`, {
          method: 'POST',
          body: JSON.stringify({name: userName}),
          headers: {'Content-Type': 'application/json'}
        })
        .then(response => response.json())
        .then(data => setUser({
          id: data.id,
          name: data.name
        }))
      } else if (game && misteryNumber){
        fetch(`${apiBaseURL}/player/${user?.id}/attempt/${misteryNumber}`, {
          method: 'POST',
          headers: {'Content-Type': 'application/json'}
        })
        .then(response => response.json())
        .then(data => setAttempt({
          correctAnswer: data.correctAnswer,
          attempts: data.attempts,
          isGreater: data.isGreater
        }))
      }
    }
  }

  const handleStart = (event: MouseEvent<HTMLButtonElement>) => {
    fetch(`${apiBaseURL}/player/${user?.id}/game`, {
      method: 'GET',
      headers: {'Content-Type': 'application/json'}
    })
    .then(response => response.json())
    .then(data => setGame({
      min: data.min,
      max: data.max
    }))
  }

  const handleStartAgain = (event: MouseEvent<HTMLButtonElement>) => {
    setAttempt(undefined);
    setMisteryNumber(null);

    fetch(`${apiBaseURL}/player/${user?.id}/game`, {
      method: 'GET',
      headers: {'Content-Type': 'application/json'}
    })
    .then(response => response.json())
    .then(data => setGame({
      min: data.min,
      max: data.max
    }))
  }

  useEffect(() => {console.log(user)}, [user])

  if (!user){
    return (
      <Wrapper>
        <Title>Hi there! Welcome to Hi-Lo Game</Title>
        <RequestNameMessage>Please, fill in your name! :D then, press Enter</RequestNameMessage>
        <Input value={userName} onChange={evt => setUserName(evt.target.value)} onKeyDown={handleKeyDown} />
      </Wrapper>
    );
  } else if (!game) {
    return <Wrapper>
      <Title>Hi {user.name}! Welcome to Hi-Lo Game</Title>
      <RequestNameMessage>Please, press the start button to start the game</RequestNameMessage>
      <StartButton onClick={handleStart}>Start</StartButton>
    </Wrapper>
  } else {
    return <Wrapper>
      <RequestNameMessage>Please, fill in a number between {game.min} and {game.max} then press Enter</RequestNameMessage>
      <Input disabled={attempt?.correctAnswer} type="number" value={misteryNumber ?? ''} onChange={evt => setMisteryNumber(+evt.target.value)} onKeyDown={handleKeyDown} />
      {attempt && !attempt.correctAnswer && attempt.isGreater && <RequestNameMessage size='small'>Hi: the mystery number is {'>'} the player's guess</RequestNameMessage>}
      {attempt && !attempt.correctAnswer && !attempt.isGreater && <RequestNameMessage size='small'>LO: the mystery number is{`<`} the player's guess</RequestNameMessage>}
      {attempt && attempt.correctAnswer && <RequestNameMessage size='small'>Congratulations!!! You've answered correcly after {attempt.attempts} attempts</RequestNameMessage>}
      {attempt && attempt.correctAnswer && <StartButton onClick={handleStartAgain}>Start again</StartButton>}
    </Wrapper>
  }
}

export default App;
