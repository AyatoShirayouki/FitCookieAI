import React from 'react';

interface DietPlanProps {
    loginResponse: any,
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

const DietPlan = ({ loginResponse}: DietPlanProps) => {
    console.log(loginResponse);
    return (
        <div className='diet-plan__container'>
            <h3 style={header3Styles}>Welcome</h3>
            {loginResponse && 'body' in loginResponse && loginResponse.body && <h3 style={header3Styles}>{loginResponse.body.firstName} {loginResponse.body.lastName}</h3>}
            <h3 style={header3Styles}>Click 'Generate' and get your personalized diet plan!</h3>
        </div>
    )
};

export default DietPlan;