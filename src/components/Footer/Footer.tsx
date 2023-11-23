import React from 'react';
import { Link, useLocation } from 'react-router-dom';

const footerContentWrapperStyles: React.CSSProperties = {
    paddingLeft: 100,
    paddingRight: 100,
    boxSizing: 'border-box',
};

const socialMediaAnchorStyles: React.CSSProperties = {
    padding: 15,
    border: '1px solid rgba(256,256,256,.1)',
    borderRadius: '50%',
    color: '#bf5b00',
};

const addressQuickLinksStyles: React.CSSProperties = {
    display: 'flex',
    justifyContent: 'space-between',
    width: '50%',
    marginBottom: 25
};

const chevronUpStyles: React.CSSProperties = {
    transform: 'rotate(90deg)',
};

const navigationStyles: React.CSSProperties = {
    fontWeight: 600,
    color: 'rgb(191, 91, 0)',
    textDecoration: 'none',
    outline: 'none',
};

const addressStyles: React.CSSProperties = {
    width: '50%',
};

const header4Styles: React.CSSProperties = {
    fontSize: 'calc(1.275rem + .3vw)',
    color: '#fff',
};

const addressParagraphStyles: React.CSSProperties = {
    color: "#fff",
};

const addressQuickLinksSocialMediasStyles: React.CSSProperties = {
    marginBottom: '2rem',
};

const copyRightContainer: React.CSSProperties = {
    display: 'flex',
    padding: '25px 0',
    borderTop: '1px solid rgba(256,256,256,.1)'
};

const copyRightParagraph: React.CSSProperties = {
    width: '50%',
    color: '#fff',
};

const copyRightAnchor: React.CSSProperties = {
    borderBottom: '1px solid #dee2e6',
    textDecoration: 'none',
    color: '#bf5b00',
};

const poweredByOpenAI: React.CSSProperties = {
    display: 'flex',
    alignItems: 'center',
    gap: 10,
    margin: 0,
    fontSize: 24,
    fontWeight: 800,
    color: '#fd8c25',
};

const Footer = ({ }) => {
    const location = useLocation();

    return (
        <footer>
            <div className='footer-content-wrapper' style={footerContentWrapperStyles}>
                <div className='address-quick-links-socia-medias' style={addressQuickLinksSocialMediasStyles}>
                    <div className='address-quick-links' style={addressQuickLinksStyles}>
                        <div className='address' style={addressStyles}>
                            <h4 style={header4Styles}>Address</h4>
                            <p style={addressParagraphStyles}><i className="fa fa-globe"></i> 15 West Street, Reading RG1 1TT United Kingdom</p>
                            <p style={addressParagraphStyles}><i className="fa fa-phone"></i> 0118 957 4680</p>
                            <p style={addressParagraphStyles}><i className="fa fa-envelope"></i> sales@fitcookie.co.uk</p>
                        </div>
                        <div className='quick-links'>
                            <h4 style={header4Styles}>Quick links</h4>
                            <div style={{ display: 'flex', flexDirection: 'column', gap: 20 }}>
                                {location.pathname.includes('ResetPassword') && <Link to="/" style={navigationStyles}><i className="fa fa-chevron-up" style={chevronUpStyles}></i>FitCookieAI</Link>}
                                {
                                    !location.pathname.includes('ResetPassword') &&
                                    <>
                                        <a href="#about" style={navigationStyles}><i className="fa fa-chevron-up" style={chevronUpStyles}></i> About Us</a>
                                        <a href="#guide" style={navigationStyles}><i className="fa fa-chevron-up" style={chevronUpStyles}></i> Guide</a>
                                        <a href="#features" style={navigationStyles}><i className="fa fa-chevron-up" style={chevronUpStyles}></i> Features</a>
                                        <a href="#dietPlanGenerator" style={navigationStyles}><i className="fa fa-chevron-up" style={chevronUpStyles}></i> Diet plan generator</a>
                                    </>
                                }
                            </div>
                        </div>
                    </div>
                    <div className='social-medias'>
                        <a href="#" style={socialMediaAnchorStyles}><i className="fa fa-twitter"></i></a>
                        <a href="#" style={socialMediaAnchorStyles}><i className="fa fa-facebook-f"></i></a>
                        <a href="#" style={socialMediaAnchorStyles}><i className="fa fa-instagram"></i></a>
                        <a href="#" style={socialMediaAnchorStyles}><i className="fa fa-linkedin"></i></a>
                    </div>
                </div>
                <div className='copyright-container' style={copyRightContainer}>
                    <p style={copyRightParagraph}>© <a href="#" style={copyRightAnchor}>FitCookieAI</a>, All Right Reserved. Designed By <a href="#" style={copyRightAnchor}>Alexander Nestorov</a>. Distributed By <a href="#" style={copyRightAnchor}>Rosen Rosenov</a>.</p>
                    <h1 className='powered-by-open-ai' style={poweredByOpenAI}>Powered by <img src='https://i.ibb.co/NnvyZzQ/open-AI-logo.png' width={50} /> Open AI</h1>
                </div>
            </div>
        </footer>
    )
};

export default Footer;