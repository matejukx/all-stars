import React, { useState } from 'react';
import './styles.css'; // For traditional CSS approach
import { login } from './api';


export const Header = () => {
  const [isModalOpen, setModalOpen] = useState(false);
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  const handleLogin = async () => {
      const token = await login(username, password)
      
      if (token){
        setModalOpen(false);
        setIsLoggedIn(true);
      }
      else {
        setError("Nie udało się zalogować.")
      }
      
  };

  const onLoginLogout = () => {
    setIsLoggedIn(false);
    // remove token
  }

    return (
      <header className="header">
        <div className="header__app-name">
          <span>Aplikacja Gwiazd</span>
          {isLoggedIn && (
            <button className="header__button button__green">
              Dodaj Grę
            </button>)}
        </div>
        <div className="header__auth">
          {isLoggedIn ? (
            <button className="header__button" onClick={onLoginLogout}>
              Logout
            </button>
          ) : (
            <button className="header__button" onClick={() => setModalOpen(true)}>
              Login
            </button>
          )}
        </div>

        {isModalOpen && (
                <div className="modal-overlay">
                    <div className="modal">
                        <h2>Login</h2>
                        <input
                            type="text"
                            placeholder="Username"
                            value={username}
                            onChange={(e) => setUsername(e.target.value)}
                        />
                        <input
                            type="password"
                            placeholder="Password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />
                        {error}
                        <br/>
                        <button onClick={handleLogin}>Login</button>
                        <button onClick={() => setModalOpen(false)}>Close</button>
                    </div>
                </div>
            )}
      </header>

      
    );
  };