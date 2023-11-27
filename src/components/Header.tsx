import React, { useEffect, useState } from 'react';
import ScrollToTopButton from './ScrollToTopButton/ScrollToTopButton';

interface HeaderProps {
    isLoggedIn: boolean,
    setIsLoggedIn: Function,
    loginResponse: any,
    setLoginResponse: Function
};

const headerStyles: React.CSSProperties = {
    display: 'flex',
    justifyContent: 'space-around',
    position: 'absolute',
    width: '100%',
    top: 0,
    left: 0,
    right: 9,
    padding: '25px 0',
    zIndex: 999,
};

const mobileHeaderStyles: React.CSSProperties = {
    display: 'flex',
    flexDirection: 'column',
    gap: 15,
    padding: '15px 25px',
    backgroundColor: '#fff',
    boxSizing: 'border-box',
};

const logoHamburgerButtonStyles: React.CSSProperties = {
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
    width: '100%',
};

const headerYOffsetStyles: React.CSSProperties = {
    display: 'flex',
    justifyContent: 'space-around',
    position: 'fixed',
    width: '100%',
    top: 0,
    left: 0,
    right: 9,
    padding: '10px 0',
    backgroundColor: '#fff',
    boxShadow: '0 0.125rem 0.25rem rgba(0,0,0,0.075)',
    transition: 'all 0.5s ease-in-out',
    zIndex: 999,
};

const navigationStyles: React.CSSProperties = {
    display: 'flex',
    gap: 25,
    margin: 'auto 0',
    color: '#bf5b00',
};

const mobileNavigationStyles: React.CSSProperties = {
    display: 'flex',
    flexDirection: 'column',
    alignSelf: 'flex-start',
    gap: 25,
    color: '#bf5b00',
};

const navigationItemStyles: React.CSSProperties = {
    alignSelf: 'flex-start',
    fontWeight: 600,
    color: '#bf5b00',
    textDecoration: 'none',
    outline: 'none',
};

const generateDietPlanButtonStyles: React.CSSProperties = {
    margin: 'auto 0',
    padding: '0.5rem 1.5rem',
    borderRadius: '50rem',
    color: '#fff',
    background: 'linear-gradient(to bottom right, #974700, #fbbc34)',
    textDecoration: 'none',
    outline: 'none',
};

const hamburgerBtnOutline: React.CSSProperties = {
    display: 'flex',
    flexDirection: 'column',
    gap: 5,
    padding: 10,
    border: '1px solid rgba(0,0,0,0.2)',
    borderRadius: 10,
    cursor: 'pointer',
};

const hamburgerTopBtnLine: React.CSSProperties = {
    width: 20,
    height: 2.1,
    backgroundColor: 'rgba(0,0,0,0.55)',
};

const hamburgerBtnLines: React.CSSProperties = {
    width: 20,
    height: 2,
    backgroundColor: 'rgba(0,0,0,0.55)'
};

const Header = ({ isLoggedIn, setIsLoggedIn, loginResponse, setLoginResponse }: HeaderProps) => {

    const [newHeaderStyles, setNewHeaderStyles] = useState(headerStyles);
    const [mobileNavigationOpened, setMobileNavigationOpened] = useState(false);

    const toggleMobileNavigationMenu = () => {
        setMobileNavigationOpened((prevValue) => !prevValue);
    };

    const getWindowYOffset = () => {
        if (window.pageYOffset > 40) {
            setNewHeaderStyles(headerYOffsetStyles);
            return;
        }
        setNewHeaderStyles(headerStyles);
    };

    const logout = async () => {
        console.log(loginResponse);
        const { body: { id: userId } } = loginResponse;
        await fetch(`http://fitcookieai.uksouth.cloudapp.azure.com:8087/api/Users/Logout?userId=${userId}`);
        // localStorage.removeItem('email');
        // localStorage.removeItem('password');
        setIsLoggedIn(false);
    };

    useEffect(() => {
        window.addEventListener('scroll', getWindowYOffset);

        return () => {
            window.removeEventListener('scroll', getWindowYOffset);
        }
    }, []);

    return (
        <>
            {window.pageYOffset > 40 && <ScrollToTopButton />}
            <header className='main-header' style={newHeaderStyles}>
                <a href="#">
                    <img src='https://i.ibb.co/fCH3XLr/Logo-350-350px-1-1.png' />
                </a>
                <nav className='navigation' style={navigationStyles}>
                    <a href='#' style={navigationItemStyles}>Home</a>
                    <a href='#about' style={navigationItemStyles}>About</a>
                    <a href='#guide' style={navigationItemStyles}>Guide</a>
                    <a href='#features' style={navigationItemStyles}>Features</a>
                    <a href='#dietPlanGenerator' style={navigationItemStyles}>Diet plan generator</a>
                </nav>
                <div style={{ display: 'flex', gap: 15, margin: 'auto 0' }}>
                    <a href="#dietPlanGenerator" className='gradient-btn generate-diet-plan-btn' style={generateDietPlanButtonStyles}>Generate a diet plan</a>
                    {isLoggedIn && <a className='gradient-btn generate-diet-plan-btn' style={{ ...generateDietPlanButtonStyles, cursor: 'pointer' }} onClick={async () => { await logout() }}>Logout</a>}
                </div>
            </header>
            <header className='main-mobile-header' style={mobileHeaderStyles}>
                <div className='img-hamburger-button__container' style={logoHamburgerButtonStyles}>
                    <a href="#">
                        <img src='https://i.ibb.co/fCH3XLr/Logo-350-350px-1-1.png' />
                    </a>
                    <div className='hamburger-btn-outline' style={hamburgerBtnOutline} onClick={toggleMobileNavigationMenu}>
                        <div className='hamburger-top-line' style={hamburgerTopBtnLine}></div>
                        <div className='hamburger-middle-line' style={hamburgerBtnLines}></div>
                        <div className='hamburger-bottom-line' style={hamburgerBtnLines}></div>
                    </div>
                </div>
                <div id="mobileNavWrapper" style={{
                    height: mobileNavigationOpened ? isLoggedIn ? '39vh' : '29vh' : 0,
                    overflow: 'hidden',
                    transition: '.5s'
                }}>
                    <div className='break-line' style={{ width: '100%', height: 0.5, background: 'rgba(0,0,0,0.2)', marginBottom: 15 }}></div>
                    <nav className='navigation' style={mobileNavigationStyles}>
                        <a href='#' style={navigationItemStyles}>Home</a>
                        <a href='#about' style={navigationItemStyles}>About</a>
                        <a href='#guide' style={navigationItemStyles}>Guide</a>
                        <a href='#features' style={navigationItemStyles}>Features</a>
                        <a href='#dietPlanGenerator' style={navigationItemStyles}>Diet plan generator</a>
                        {isLoggedIn && <a className='gradient-btn generate-diet-plan-btn' style={{ ...generateDietPlanButtonStyles, cursor: 'pointer', alignSelf: 'flex-start' }} onClick={async () => { await logout() }}>Logout</a>}
                    </nav>
                </div>
            </header>
        </>
    )
};

export default Header;