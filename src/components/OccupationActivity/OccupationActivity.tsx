import React from 'react';

interface OccupationActivityProps {
    occupation: string,
    setOccupation: Function,
    activityLevel: string,
    setActivityLevel: Function,
    options: { option_id: number, text: string, value: string }[]
};

const labelStyles: React.CSSProperties = {
    position: 'absolute',
    top: 0,
    left: 0,
    padding: '1rem 0.5rem 1rem 0',
    color: '#919294',
    pointerEvents: 'none',
    transform: 'scale(0.85) translateY(-0.5rem) translateX(0.15rem)',
    transition: 'opacity 0.1s ease-in-out,transform 0.1s ease-in-out',
    opacity: .65,
};

const smallTextStyles: React.CSSProperties = {
    float: 'left',
    marginTop: 5,
    marginLeft: 5,
    color: '#fbbc34'
};

const occupationActivityContainer: React.CSSProperties = {
    display: 'flex',
    justifyContent: 'space-between',
};

const occupationActivityDivsStyles: React.CSSProperties = {
    position: 'relative',
    width: '45%'
};

const occupationInputStyles: React.CSSProperties = {
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
    padding: '1.9rem 2.25rem 1rem 0.75rem ',
    fontSize: '1rem',
    fontWeight: 400,
    lineHeight: 1.5,
    color: '#919294',
    border: '1px solid #ced4da',
    borderRadius: 10,
    boxSizing: 'border-box'
};

const OccupationActivity = ({ occupation, setOccupation, activityLevel, setActivityLevel, options }: OccupationActivityProps) => {

    const handleOccupationStateUpdate = (event: any) => {
        setOccupation(event.target.value);
    };

    const handleActivityLevelState = (event: any) => {
        setActivityLevel(event.target.value);
    }

    const extractOptions = () => {
        return options.map((option: { option_id: number, text: string, value: string }) => <option key={option.option_id} value={option.value}>{option.text}</option>)
    };

    return (
        <div className='occupation-activity__container' style={occupationActivityContainer}>
            <div className='occupation' style={occupationActivityDivsStyles}>
                <input id='occupation' type='text' style={occupationInputStyles} value={occupation} onChange={handleOccupationStateUpdate} />
                <label htmlFor="occupation" style={labelStyles}>Your Current Occupation</label>
                <small style={smallTextStyles}>required</small>
            </div>
            <div className='activity' style={occupationActivityDivsStyles}>
                <label htmlFor="activity" style={labelStyles}>Activity Level</label>
                <select name="activity" id="activity" style={selectStyles} value={activityLevel} onChange={handleActivityLevelState}>
                    {extractOptions()}
                </select>
            </div>
        </div>
    )
};

export default OccupationActivity;