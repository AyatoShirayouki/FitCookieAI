import React from 'react';
import Header from './Header';

interface JumbotronProps {
    isLoggedIn: boolean,
    setIsLoggedIn: Function,
    loginResponse: any,
    setLoginResponse: Function
};

const jumbotronTextContainerStyles: React.CSSProperties = {
    width: '66.6%',
    paddingTop: '16rem',
    paddingLeft: '7.2rem',
    color: '#fff',
};

const jumbotronTextContainerHeaderStyles: React.CSSProperties = {
    fontSize: 45,
};

const jumbotronTextContainerParagraphStyles: React.CSSProperties = {
    marginBottom: 45,
    lineHeight: 1.5,
};

const readMoreButtonStyles: React.CSSProperties = {
    margin: 'auto 0',
    padding: '1rem 3rem',
    borderRadius: '50rem',
    color: '#fff',
    background: 'linear-gradient(to bottom right, #974700, #fbbc34)',
    textDecoration: 'none',
    outline: 'none',
};

const Jumbotron = ({isLoggedIn, setIsLoggedIn, loginResponse, setLoginResponse }: JumbotronProps) => {
    return (
        <div className='jumbotron'>
            <Header isLoggedIn={isLoggedIn} setIsLoggedIn={setIsLoggedIn} loginResponse={loginResponse} setLoginResponse={setLoginResponse} />
            <div className='jumbotron-text-container' style={jumbotronTextContainerStyles}>
                <h1 style={jumbotronTextContainerHeaderStyles}>Fit Cookie - Your Personal Nutritionist</h1>
                <p style={jumbotronTextContainerParagraphStyles}>Welcome to the future of personalized nutrition. We've taken the guesswork out of healthy eating, providing you with custom-made, scientifically-backed diet plans tailored to your needs and lifestyle!</p>
                <a href="#about" className='gradient-btn read-more-btn' style={readMoreButtonStyles}>Read More</a>
            </div>
        </div>
    )
};

export default Jumbotron;