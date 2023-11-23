import React from 'react';

interface FeatureCardProps {
    iconClass: string,
    header: string,
    paragraph: string,
};

const featureCardStyles: React.CSSProperties = {
    width: '33.3%',
    position: 'relative',
    padding: '1.5rem',
    textAlign: 'left',
    backgroundColor: '#F0F6FF',
    borderRadius: 10,
};

const imgHolderStyles: React.CSSProperties = {
    width: 50,
    height: 50,
    display: 'inline-flex',
    justifyContent: 'center',
    alignItems: 'center',
    marginBottom: 15,
    background: 'linear-gradient(to bottom right, #fbbc34, #fd8c25)',
    borderRadius: '50%',
    boxShadow: '0 0.5rem 1rem rgba(0,0,0,0.15)',
};

const headerStyles: React.CSSProperties = {
    marginTop: 0,
    marginBottom: 15,
    fontSize: '1.25rem',
    color: '#262B47',
};

const paragraphStyles: React.CSSProperties = {
    textAlign: 'justify',
    lineHeight: 1.5,
    color: '#919294',
};

const iconStyle: React.CSSProperties = {
    fontSize: 'calc(1.275rem + .3vw)',
    color: '#fff',
};

const FeatureCard = ({ iconClass, header, paragraph }: FeatureCardProps) => {
    return (
        <div className='card feature-card' style={featureCardStyles}>
            <div className='feature-card-img-holder' style={imgHolderStyles}>
                <i className={iconClass} style={iconStyle}></i>
            </div>
            <h5 style={headerStyles}>{header}</h5>
            <p style={paragraphStyles}>{paragraph}</p>
        </div>
    )
};

export default FeatureCard;