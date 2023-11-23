import React from 'react';

const scrollToTopButton: React.CSSProperties = {
    position: 'fixed',
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    right: 45,
    bottom: 45,
    width: 48,
    height: 48,
    background: 'linear-gradient(to bottom right, #fd8c25, #fbbc34)',
    color: '#fff',
    textDecoration: 'none',
    borderRadius: 50,
    zIndex: 999,
};

const ScrollToTopButton = ({ }) => {
    return (
        <a href='#' className='scroll-to-top-button' style={scrollToTopButton}>TOP</a>
    )
};

export default ScrollToTopButton;