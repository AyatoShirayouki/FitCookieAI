import React from 'react';

interface StepCardProps {
    imgURL: string,
    header: string,
    paragraph: string,
};

const stepCardStyles: React.CSSProperties = {
    width: '33.3%',
    position: 'relative',
    padding: '3rem 1.5rem 1rem 1.5rem',
    textAlign: 'center',
    backgroundColor: '#F0F6FF',
    borderRadius: 10,
};

const imgHolderStyles: React.CSSProperties = {
    position: 'absolute',
    top: 0,
    width: 100,
    height: 100,
    display: 'inline-flex',
    justifyContent: 'center',
    alignItems: 'center',
    background: 'linear-gradient(to bottom right, #fbbc34, #fd8c25)',
    borderRadius: '50%',
    transform: 'translate(-50%, -50%)',
    boxShadow: '0 0.5rem 1rem rgba(0,0,0,0.15)',
};

const headerStyles: React.CSSProperties = {
    marginBottom: 15,
    fontSize: '1.25rem',
    color: '#262B47',
};

const paragraphStyles: React.CSSProperties = {
    textAlign: 'justify',
    lineHeight: 1.5,
    color: '#919294',
};

const imageStyles: React.CSSProperties = {
    width: 135,
    height: 'auto',
};

const StepCard = ({ imgURL, header, paragraph }: StepCardProps) => {
    return (
            <div className='step-card' style={stepCardStyles}>
                <div className='step-card-img-holder' style={imgHolderStyles}>
                    <img src={imgURL} style={imageStyles} />
                </div>
                <h5 style={headerStyles}>{header}</h5>
                <p style={paragraphStyles}>{paragraph}</p>
            </div>
    )
};

export default StepCard;