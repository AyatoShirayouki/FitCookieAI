import React, { useState } from 'react';
import './App.css';
import Jumbotron from './components/Jumbotron';
import About from './components/About';
import Wrapper from './components/Wrapper';
import Guide from './components/Guide';
import Features from './components/Features/Features';
import Footer from './components/Footer/Footer';
import DietPlanGenerator from './components/DietPlanGenerator/DietPlanGenerator';
import ResetPassword from './components/ResetPassword/ResetPassword';
import { BrowserRouter as Router, Route, Link, Routes } from 'react-router-dom';

const App = () => {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [loginResponse, setLoginResponse] = useState<any>(null);

  return (
    <div className="fit-cookie-app" style={{ fontFamily: '"Jost",sans-serif' }}>
      <Router>
        <Routes>
          <Route path="/" element={
            <>
              <Jumbotron isLoggedIn={isLoggedIn} setIsLoggedIn={setIsLoggedIn} loginResponse={loginResponse} setLoginResponse={setLoginResponse} />
              <Wrapper>
                <About />
                <Guide />
                <Features />
                <DietPlanGenerator isLoggedIn={isLoggedIn} setIsLoggedIn={setIsLoggedIn} loginResponse={loginResponse} setLoginResponse={setLoginResponse} />
              </Wrapper>
              <Footer />
            </>
          } />
          <Route path="ResetPassword" element={<ResetPassword loginResponse={loginResponse} />} />
        </Routes>
      </Router>
    </div>
  );
}

export default App;
