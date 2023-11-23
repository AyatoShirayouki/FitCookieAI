import React, { useState } from 'react';
import { validEmailReg, validateInput, validatePhoneNumber } from '../DietPlanGenerator/DietPlanGenerator';
import TermsAndConditions from './TermsAndConditions';
import { Link } from 'react-router-dom';

interface FormProps {
    isRegistered: boolean,
    setIsRegistered: Function,
    loginData: { email: string, password: string },
    setLoginData: Function,
    signUpData: { Email: string, Password: string, confirmPassword: string, FirstName: string, LastName: string, Gender: string, DOB: Date | null, PhoneNumber: string, DOJ: Date },
    setSignUpData: Function,
    termsAndConditionsAccepted: boolean,
    setTermsAndConditionsAccepted: Function,
    signUpError: boolean,
};

const header3Styles: React.CSSProperties = {
    marginTop: 0,
    marginBottom: '0.5rem',
    fontWeight: 600,
    fontSize: 'calc(1.3rem + .6vw)',
    textAlign: 'center',
    lineHeight: 1.2,
    color: '#262B47',
};

const radioButtonsContainerStyles: React.CSSProperties = {
    display: 'flex',
    flexDirection: 'column',
    marginBottom: 15
};

const radioButtonHolderStyles: React.CSSProperties = {
    display: 'flex'
};

const loginContainerStyles: React.CSSProperties = {
    display: 'flex',
    justifyContent: 'space-between',
    marginBottom: 5,
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

const smallTextStyles: React.CSSProperties = {
    // float: 'left',
    display: 'flex',
    marginTop: 5,
    marginLeft: 5,
    color: '#fbbc34'
};

const inputDivsStyles: React.CSSProperties = {
    position: 'relative',
    width: '45%'
};

const inputDivsFullWidthStyles: React.CSSProperties = {
    position: 'relative',
    marginBottom: 5,
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

const selectStyles: React.CSSProperties = {
    display: 'block',
    width: '100%',
    padding: '1.9rem 1.05rem 0.75rem 0.6rem ',
    fontSize: '1rem',
    fontWeight: 400,
    lineHeight: 1.5,
    color: '#919294',
    border: '1px solid #ced4da',
    borderRadius: 10,
    boxSizing: 'border-box'
};

const Form = ({ isRegistered, setIsRegistered, loginData, setLoginData, signUpData, setSignUpData, termsAndConditionsAccepted, setTermsAndConditionsAccepted, signUpError }: FormProps) => {

    const handleRadioButtonsStateChange = (event: any) => {
        if (event.target.value === 'login') { setIsRegistered(true); return; }
        setIsRegistered(false);
    };

    const setLoginCredentials = (event: any, bool?: boolean) => {
        setLoginData((prevData: { email: string, password: string }) => {
            if (bool) prevData.email = event.target.value;
            else prevData.password = event.target.value;
            return prevData;
        })
    };

    const setSignUpCredentials = (event: any, type: string) => {
        setSignUpData((prevData: { Email: string, Password: string, confirmPassword: string, FirstName: string, LastName: string, Gender: string, DOB: Date | null, PhoneNumber: string, DOJ: Date }) => {
            if (type === 'email') prevData.Email = event.target.value;
            else if (type === 'firstName') prevData.FirstName = event.target.value;
            else if (type === 'lastName') prevData.LastName = event.target.value;
            else if (type === 'password') prevData.Password = event.target.value;
            else if (type === 'confirmPassword') prevData.confirmPassword = event.target.value;
            else if (type === 'gender') prevData.Gender = event.target.value;
            else if (type === 'dateOfBirth') prevData.DOB = event.target.value;
            else prevData.PhoneNumber = event.target.value;
            return prevData;
        })
    };

    return (
        <div className='forms__container'>
            <h3 style={header3Styles}>Welcome</h3>
            <div className='radio-buttons__container' style={radioButtonsContainerStyles}>
                <div style={radioButtonHolderStyles}>
                    <input type="radio" id="login" name="forms" value="login" checked={isRegistered} onChange={handleRadioButtonsStateChange} />
                    <label htmlFor="login">I have an account</label>
                </div>
                <div style={radioButtonHolderStyles}>
                    <input type="radio" id="signup" name="forms" value="signup" checked={!isRegistered} onChange={handleRadioButtonsStateChange} />
                    <label htmlFor="signup">I don't have an account</label>
                </div>
            </div>
            {/* Login Form */}
            {isRegistered &&
                <>
                    <div className='login-form__container' style={loginContainerStyles}>
                        <div className='email-login' style={inputDivsStyles}>
                            <input id='emailLogin' type='email' style={loginInputStyles} onChange={(event) => { setLoginCredentials(event, true) }} />
                            <label htmlFor="emailLogin" style={labelStyles}>Email</label>
                            <small style={smallTextStyles}>required</small>
                        </div>
                        <div className='password-login' style={inputDivsStyles}>
                            <input id='passwordLogin' type='password' style={loginInputStyles} onChange={(event) => { setLoginCredentials(event) }} />
                            <label htmlFor="passwordLogin" style={labelStyles}>Password</label>
                            <small style={smallTextStyles}>required</small>
                        </div>
                    </div>
                    <div style={{textAlign: 'left'}}><Link to="/ResetPassword">Forgot your password?</Link></div>
                </>
            }
            {/* Sign Up Form */}
            {!isRegistered &&
                <div className='sign-up__container'>
                    <div className='email-sign-up-holder' style={inputDivsFullWidthStyles}>
                        <input id='emailSignUp' type='text' style={loginInputStyles} onChange={(event) => { setSignUpCredentials(event, 'email') }} />
                        <label htmlFor="emailSignUp" style={labelStyles}>Email</label>
                        <small style={smallTextStyles}>required</small>
                        {signUpError && (signUpData.Email === '' || !signUpData.Email.match(validEmailReg)) && <p style={{ marginTop: 3, marginLeft: 5, textAlign: 'left', color: '#ff1212' }}>Email must be in format "youremail@domain.com"</p>}
                    </div>

                    <div className='names' style={loginContainerStyles}>
                        <div className='first-name-holder' style={inputDivsStyles}>
                            <input id='firstName' type='text' style={loginInputStyles} onChange={(event) => { setSignUpCredentials(event, 'firstName') }} />
                            <label htmlFor="firstName" style={labelStyles}>First Name</label>
                            <small style={smallTextStyles}>required</small>
                            {signUpError && !validateInput(signUpData.FirstName, 1) && <p style={{ marginTop: 3, marginLeft: 5, textAlign: 'left', color: '#ff1212' }}>Field cannot be blank</p>}
                        </div>
                        <div className='last-name-holder' style={inputDivsStyles}>
                            <input id='lastName' type='text' style={loginInputStyles} onChange={(event) => { setSignUpCredentials(event, 'lastName') }} />
                            <label htmlFor="lastName" style={labelStyles}>Last Name</label>
                            <small style={smallTextStyles}>required</small>
                            {signUpError && !validateInput(signUpData.LastName, 1) && <p style={{ marginTop: 3, marginLeft: 5, textAlign: 'left', color: '#ff1212' }}>Field cannot be blank</p>}
                        </div>
                    </div>

                    <div className='passwords' style={loginContainerStyles}>
                        <div className='password-holder' style={inputDivsStyles}>
                            <input id='password' type='password' style={loginInputStyles} onChange={(event) => { setSignUpCredentials(event, 'password') }} />
                            <label htmlFor="password" style={labelStyles}>Password</label>
                            <small style={smallTextStyles}>required</small>
                            {signUpError && !validateInput(signUpData.Password, 6) && <p style={{ marginTop: 3, marginLeft: 5, textAlign: 'left', color: '#ff1212' }}>Password must be at least 6 characters long</p>}
                        </div>
                        <div className='confirm-password-holder' style={inputDivsStyles}>
                            <input id='confirmPassword' type='password' style={loginInputStyles} onChange={(event) => { setSignUpCredentials(event, 'confirmPassword') }} />
                            <label htmlFor="confirmPassword" style={labelStyles}>Confirm Password</label>
                            <small style={smallTextStyles}>required</small>
                            {signUpError && signUpData.Password !== signUpData.confirmPassword && <p style={{ marginTop: 3, marginLeft: 5, textAlign: 'left', color: '#ff1212' }}>Passwords do not match</p>}
                        </div>
                    </div>

                    <div className='gender-date-of-birth' style={loginContainerStyles}>
                        <div className='gender-holder' style={inputDivsStyles}>
                            <label htmlFor="health-goals" style={labelStyles}>Gender</label>
                            <select name="health-goals" id="health-goals" style={selectStyles} value={signUpData.Gender} onChange={(event) => { setSignUpCredentials(event, 'gender') }}>
                                <option value='male'>Male</option>
                                <option value='female'>Female</option>
                            </select>
                            <small style={smallTextStyles}>required</small>
                        </div>
                        <div className='date-of-birth-holder' style={inputDivsStyles}>
                            <input id='dateOfBirth' type='date' style={loginInputStyles} onChange={(event) => { setSignUpCredentials(event, 'dateOfBirth') }} />
                            <label htmlFor="dateOfBirth" style={labelStyles}>Date Of Birth</label>
                            <small style={smallTextStyles}>required</small>
                            {signUpError && !signUpData.DOB && <p style={{ marginTop: 3, marginLeft: 5, textAlign: 'left', color: '#ff1212' }}>Please choose your date of birth</p>}
                        </div>
                    </div>

                    <div className='phone-number-holder' style={inputDivsFullWidthStyles}>
                        <input id='phoneNumber' type='text' style={loginInputStyles} onChange={(event) => { setSignUpCredentials(event, 'phoneNumber') }} />
                        <label htmlFor="phoneNumber" style={labelStyles}>Phone Number</label>
                        <small style={smallTextStyles}>required</small>
                        {signUpError && signUpData.PhoneNumber.length === 0 && <p style={{ marginTop: 3, marginLeft: 5, textAlign: 'left', color: '#ff1212' }}>Field cannot be blank</p>}
                        {signUpError && signUpData.PhoneNumber.length > 0 && signUpData.PhoneNumber.length <= 20 && !validatePhoneNumber(signUpData.PhoneNumber) && <p style={{ marginTop: 3, marginLeft: 5, textAlign: 'left', color: '#ff1212' }}>Phone number must be in format "(0) 888 888 8888" or "(+country code) 888 888 8888"</p>}
                        {signUpError && signUpData.PhoneNumber.length > 0 && signUpData.PhoneNumber.length > 20 && !validatePhoneNumber(signUpData.PhoneNumber) && <p style={{ marginTop: 3, marginLeft: 5, textAlign: 'left', color: '#ff1212' }}>Phone number must not exceed 20 digits</p>}
                    </div>

                    <TermsAndConditions signUpError={signUpError} termsAndConditionsAccepted={termsAndConditionsAccepted} setTermsAndConditionsAccepted={setTermsAndConditionsAccepted} />
                </div>
            }
        </div>
    )
};

export default Form;