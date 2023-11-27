import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import Footer from '../Footer/Footer';

interface ResetPasswordProps {
    loginResponse: any
}

const stepsHeaderStyles: React.CSSProperties = {
    margin: 0,
    fontSize: 20,
    fontWeight: 500,
    color: '#fbbc34',
};

const headerStyles: React.CSSProperties = {
    marginTop: 8,
    marginBottom: '2rem',
    fontSize: 'calc(1.375rem + 1.5vw)',
    color: '#262B47',
};

const inputDivsHalfWidthStyles: React.CSSProperties = {
    position: 'relative',
    width: '50%',
    margin: '0px auto 35px auto',
};

const loginInputStyles: React.CSSProperties = {
    display: 'block',
    width: '100%',
    padding: '1.9rem 1.05rem 0.75rem 1.05rem ',
    fontSize: '1rem',
    fontWeight: 400,
    lineHeight: 1.5,
    color: '#919294',
    border: '1px solid #ced4da',
    borderRadius: 10,
    boxSizing: 'border-box'
};

const labelStyles: React.CSSProperties = {
    position: 'absolute',
    top: 0,
    left: 0,
    padding: '1rem 0.5rem 1rem 0.5rem',
    color: '#919294',
    pointerEvents: 'none',
    transform: 'scale(0.85) translateY(-0.5rem) translateX(0.15rem)',
    transition: 'opacity 0.1s ease-in-out,transform 0.1s ease-in-out',
    opacity: .65,
};

const submitButtonStyles: React.CSSProperties = {
    margin: '0 auto 55px auto',
    padding: '1.5rem 3.5rem',
    fontSize: 19,
    borderRadius: '50rem',
    color: '#fff',
    background: 'linear-gradient(to bottom right, #974700, #fbbc34)',
    border: 'none',
    textDecoration: 'none',
    outline: 'none',
    cursor: 'pointer',
};

const smallTextStyles: React.CSSProperties = {
    // float: 'left',
    display: 'flex',
    marginTop: 5,
    marginLeft: 5,
    color: '#fbbc34'
};

const ResetPassword = ({ loginResponse }: ResetPasswordProps) => {
    // States
    const [step, setStep] = useState(1);
    const [userId, setUserId] = useState(null);
    const [email, setEmail] = useState('');
    const [responseCode, setResponseCode] = useState(null);
    const [emailCode, setEmailCode] = useState('');
    const [generatedCode, setGeneratedCode] = useState<string | null>(null);
    const [tokenId, setTokenId] = useState<number | null>(null);
    const [password, setPassword] = useState('')
    const [confirmPassword, setConfirmPassword] = useState('')
    const [passwordError, setPasswordError] = useState<string | null>(null);
    const [error, setError] = useState<string | null>(null);
    const navigate = useNavigate();

    // Vars
    const chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
    const subject = "FitCookieAI Password restore.";
    const labelText = `The code you've received via email is active for 10 minutes`;
    const passwordRule = `Your new password should include at least one number, one uppercase letter, one lowercase letter, and be between 6 and 20 characters long`;
    const headerText = step === 1 ? 'Enter your Email' : step === 2 ? `Enter the verification code that we've send via email` : `Set your new password`

    const getEmail = (event: any) => {
        setEmail(event.target.value);
    };

    const getEnteredCode = (event: any) => {
        setEmailCode(event.target.value);
    };

    const verifyEmail = async (event: any) => {
        event.preventDefault();
        try {
            const response = await axios.get(`http://fitcookieai.uksouth.cloudapp.azure.com:8087/api/Users/VerifyUserByEmail?email=${email}`,
                {
                    headers: {
                        "Content-Type": "application/json",
                    }
                });
            const { data: { code, body: id } } = response;
            if (code === 201) {
                setUserId(id);
                setResponseCode(code);
                setStep((value) => value + 1);
                if (error) setError(null);
            } else if (code === 200) setError(id);
        } catch (error) {
            console.error("Error:", error);
        }
    };

    const getPasswordRecoveryTokens = async (event: any) => {
        event.preventDefault();
        try {
            const response = await axios.get(`http://fitcookieai.uksouth.cloudapp.azure.com:8087/api/PasswordRecoveryTokens/GetAll`,
                {
                    headers: {
                        "Content-Type": "application/json",
                    }
                });

            const { data: { body: codes } } = response;
            const isCodeIn = codes.find((token: any) => token.code === generatedCode && new Date() < new Date(token.end));
            if (isCodeIn && emailCode === generatedCode) {
                setStep((value) => value + 1);
                setTokenId(isCodeIn.id);
                setGeneratedCode(null);
                if (error) setError(null);
                return;
            }
            setError("Invalid token");
        } catch (error) {
            console.error("Error:", error);
        }
    };

    const generateRandomString = () => {
        let randomString = '';
        for (let i = 0; i < 5; i++) {
            const randomIndex = Math.floor(Math.random() * chars.length);
            randomString += chars.charAt(randomIndex);
        }
        return randomString;
    };

    const verifyEmailCode = async () => {
        const code = generateRandomString();
        const messageContent = `Your recovery code is: ${code}`;
        const body: any = {
            Code: code,
            UserId: userId
        }
        try {
            await fetch("http://fitcookieai.uksouth.cloudapp.azure.com:8087/api/PasswordRecoveryTokens/Save", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(body),
            });
        } catch (error) {
            console.error("Error:", error);
        }
        try {
            const response = await axios.post(`http://fitcookieai.uksouth.cloudapp.azure.com:8087/api/PasswordRecoveryTokens/SendEmail?email=${email}&subject=${subject}&messageContent=${messageContent}`,
                {
                    headers: {
                        "Content-Type": "application/json",
                    }
                });

            const { data: { code: resCode } } = response;
            if (resCode === 201) setGeneratedCode(code);
        } catch (error) {
            console.error("Error:", error);
        }
    };

    useEffect(() => {
        if (responseCode && responseCode === 201) {
            (async () => {
                await verifyEmailCode();
            })();
        }
    }, [responseCode]);

    const isValidPassword = (event: any, password: string) => {
        event.preventDefault();
        if (password.length < 6) {
            return false;
        }
        if (!/[A-Z]/.test(password)) {
            return false;
        }
        if (!/[a-z]/.test(password)) {
            return false;
        }
        return true;
    };

    const updatePassword = (event: any) => {
        setPassword(event.target.value);
    };

    const updateConfirmPassword = (event: any) => {
        setConfirmPassword(event.target.value);
    };

    const resetPassword = async (event: any) => {
        event.preventDefault();
        try {
            await axios.get(`http://fitcookieai.uksouth.cloudapp.azure.com:8087/api/Users/EditUserPassword?password=${password}&tokenId=${tokenId}`,
                {
                    headers: {
                        "Content-Type": "application/json",
                    }
                });
        } catch (error) {
            console.error("Error:", error);
        }
    };

    useEffect(() => {
        if(step !== 1) setStep(1);
    }, [])


    return (
        <section className='reset-password__container'>
            <header style={{ display: 'block', marginBottom: 65, padding: '10px 0 10px 25px', boxSizing: 'border-box' }}>
                <Link to="/">
                    <img src='https://i.ibb.co/fCH3XLr/Logo-350-350px-1-1.png' style={{ width: 200 }} />
                </Link>
            </header>
            <div className='form-wrapper' style={{ textAlign: 'center' }}>
                <h5 style={stepsHeaderStyles}>Step {step}</h5>
                <h1 style={headerStyles}>{headerText}</h1>
                <form style={{ textAlign: 'center' }}>
                    {step === 1 && <div className='input-holder' style={inputDivsHalfWidthStyles}>
                        <input id='emailResetPassword' type="email" alt="email" style={loginInputStyles} onChange={getEmail} />
                        <label htmlFor="emailSignUp" style={labelStyles}>Email</label>
                        {error && <p style={{ textAlign: 'left', color: '#ff1212' }}>{error}</p>}
                    </div>}
                    {step === 2 &&
                        <div className='input-holder' style={inputDivsHalfWidthStyles}>
                            <input id='verificationCode' type="text" alt="text" style={loginInputStyles} onChange={getEnteredCode} />
                            <label htmlFor="verificationCode" style={labelStyles}>Your verification code</label>
                            <small style={smallTextStyles}>{labelText}</small>
                            {error && <p style={{ textAlign: 'left', color: '#ff1212' }}>{error}</p>}
                        </div>
                    }
                    {step === 3 && <>
                        <div className='input-holder' style={inputDivsHalfWidthStyles}>
                            <input id='password' type="password" alt="password" style={loginInputStyles} onChange={updatePassword} />
                            <label htmlFor="password" style={labelStyles}>New password</label>
                            <small style={smallTextStyles}>{passwordRule}</small>
                            {passwordError && <p style={{ textAlign: 'left', color: '#ff1212' }}>{passwordError}</p>}
                        </div>
                        <div className='input-holder' style={inputDivsHalfWidthStyles}>
                            <input id='retypePassword' type="password" alt="password" style={loginInputStyles} onChange={updateConfirmPassword} />
                            <label htmlFor="retypePassword" style={labelStyles}>Confirm new password</label>
                        </div>
                    </>}
                    <button type='submit' style={submitButtonStyles} onClick={async (event) => {
                        if (step === 1) await verifyEmail(event)
                        else if (step === 2) await getPasswordRecoveryTokens(event);
                        else {
                            if (!isValidPassword(event, password)) { setPasswordError("Password doesn't comply with the requirements"); return; }
                            if (isValidPassword(event, password) && password !== confirmPassword) { setPasswordError("Passwords doesn't match"); return }
                            if (isValidPassword(event, password) && password === confirmPassword && tokenId) {
                                if (passwordError) setPasswordError(null);
                                await resetPassword(event);
                                navigate("../", { replace: true })
                            }
                        }
                    }}>Submit</button>
                </form>
            </div>
            <Footer />
        </section>
    )
};

export default ResetPassword;