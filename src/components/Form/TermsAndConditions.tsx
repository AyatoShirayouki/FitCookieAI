import React from 'react';

interface TermsAndConditionsProps {
    signUpError: boolean,
    termsAndConditionsAccepted: boolean,
    setTermsAndConditionsAccepted: Function
};

const TermsAndConditions = ({ signUpError, termsAndConditionsAccepted, setTermsAndConditionsAccepted }: TermsAndConditionsProps) => {
    const setTermsAndConditionsState = (event: any) => setTermsAndConditionsAccepted(event.target.checked);

    return (
        <div className='terms-and-conditions-checkbox-holder' style={{ textAlign: 'left' }}>
            <input type="checkbox" id="termsAndConditions" name="termsAndConditions" checked={termsAndConditionsAccepted} onChange={setTermsAndConditionsState} />
            <label htmlFor="termsAndConditions">I agree with the <a href="#">terms and conditions!</a></label>
            {signUpError && !termsAndConditionsAccepted && <p style={{ marginTop: 3, marginLeft: 5, textAlign: 'left', color: '#ff1212' }}>Please accept our terms and conditions</p>}
        </div>
    )
};

export default TermsAndConditions;