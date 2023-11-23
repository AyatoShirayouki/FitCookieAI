import React from 'react';
import Steps from './Steps';

const guideStyles: React.CSSProperties = {
    textAlign: 'center',
    marginBottom: 100,
};

const guideHeaderStyles: React.CSSProperties = {
    margin: 0,
    fontSize: 20,
    fontWeight: 500,
    color: '#fbbc34',
};

const headerStyles: React.CSSProperties = {
    marginTop: 8,
    marginBottom: '5.5rem',
    fontSize: 'calc(1.375rem + 1.5vw)',
    color: '#262B47',
};

const Guide = ({ }) => {
    return (
        <section id='guide' className='guide' style={guideStyles}>
            <h5 style={guideHeaderStyles}>How It Works</h5>
            <h1 style={headerStyles}>3 Easy Steps</h1>
            <Steps />
        </section>
    )
};

export default Guide;