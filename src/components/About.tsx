import React from 'react';

const sectionStyles: React.CSSProperties = {
    display: 'flex',
    marginBottom: 100,
};

const aboutHeaderStyles: React.CSSProperties = {
    margin: 0,
    fontSize: 20,
    fontWeight: 500,
    color: '#fbbc34',
};

const headerStyles: React.CSSProperties = {
    marginTop: 8,
    marginBottom: 20,
    fontSize: 'calc(1.375rem + 1.5vw)',
    lineHeight: 1.4,
    color: '#262B47',
};

const paragraphStyles: React.CSSProperties = {
    lineHeight: 1.5,
    color: '#919294',
};

const generateDietPlanButtonStyles: React.CSSProperties = {
    display: 'inline-block',
    marginTop: 9,
    padding: '0.8rem 2rem',
    borderRadius: '50rem',
    color: '#fff',
    background: 'linear-gradient(to bottom right, #974700, #fbbc34)',
    textDecoration: 'none',
    outline: 'none',
};

const About = ({ }) => {
    return (
        <section id='about' style={sectionStyles}>
            <div className='about-left'>
                <h5 style={aboutHeaderStyles}>About</h5>
                <h1 style={headerStyles}>Welcome to our AI-powered diet plan generator!</h1>
                <p style={paragraphStyles}>We believe nutrition is not one-size-fits-all. That's why our AI-enabled Diet Plan Generator uses sophisticated algorithms and real-time data to create a nutrition plan that adapts to your lifestyle. It's like having a personal nutritionist that's available 24/7!</p>
                <p style={paragraphStyles}>Your journey towards improved health and wellness begins with just a few clicks. Start with our AI-Enabled Diet Plan Generator today and embrace the revolution in personalized nutrition.</p>
                <p style={paragraphStyles}>Because when it comes to your health, you deserve nothing less than the best!</p>
                <a href="#dietPlanGenerator" className='gradient-btn about-generate-diet-plan-btn' style={generateDietPlanButtonStyles}>Generate a diet plan</a>
            </div>
            <div className='about-right'>
                <img src="https://i.ibb.co/PxD52fY/cookie-gif.gif" alt="cookie-gif" />
            </div>
        </section>
    )
};

export default About;