import React from 'react';

interface Option {
    option_id: number,
    text: string,
    value: string,
};

interface HealthGoalProps {
    options: Option[],
    healthGoal: string,
    setHealthGoal: Function,
};

const healthGoalContainerStyles: React.CSSProperties = {
    position: 'relative',
};

const selectStyles: React.CSSProperties = {
    display: 'block',
    width: '100%',
    padding: '1.9rem 2.25rem 0.75rem 0.75rem ',
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
    padding: '1rem 0.5rem',
    color: '#919294',
    pointerEvents: 'none',
    transform: 'scale(0.85) translateY(-0.5rem) translateX(0.15rem)',
    transition: 'opacity 0.1s ease-in-out,transform 0.1s ease-in-out',
    opacity: .65,
};

const HealthGoal = ({ options, healthGoal, setHealthGoal }: HealthGoalProps) => {
    const extractOptions = () => {
        return options.map((option: Option) => <option key={option.option_id} value={option.value}>{option.text}</option>)
    };

    const handleSettingHealthGoal = (event: any) => {
        if (event.target.value !== healthGoal) setHealthGoal(event.target.value);
    };

    return (
        <div className='health-goal-selector__container' style={healthGoalContainerStyles}>
            <label htmlFor="health-goals" style={labelStyles}>Health Goal</label>
            <select name="health-goals" id="health-goals" style={selectStyles} value={healthGoal} onChange={handleSettingHealthGoal}>
                {extractOptions()}
            </select>
        </div>
    );
};

export default HealthGoal;