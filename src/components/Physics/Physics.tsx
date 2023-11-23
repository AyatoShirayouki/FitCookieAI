import React, { useEffect } from 'react';

interface PhysicsProps {
    healthGoal: string,
    currentWeight: number | null,
    setCurrentWeight: Function,
    targetWeight: number | null,
    setTargetWeight: Function,
    height: number | null,
    setHeight: Function,
    BMI: number | null,
    setBMI: Function,
};

const physicsContainerStyles: React.CSSProperties = {
    display: 'flex',
    justifyContent: 'space-between',
    marginBottom: 15,
}

const physicsInputStyles: React.CSSProperties = {
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
const BMIInputStyles: React.CSSProperties = {
    display: 'block',
    width: '100%',
    padding: '1.9rem 1.05rem 0.75rem 1.05rem ',
    fontSize: '1rem',
    fontWeight: 400,
    lineHeight: 1.5,
    color: '#919294',
    backgroundColor: '#e9ecef',
    border: '1px solid #ced4da',
    borderRadius: 10,
    boxSizing: 'border-box'
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

const bmiHolderDivStyle: React.CSSProperties = {
    position: 'relative'
};

const Physics = ({ healthGoal, currentWeight, setCurrentWeight, targetWeight, setTargetWeight, height, setHeight, BMI, setBMI }: PhysicsProps) => {

    const physicsDivsStyles: React.CSSProperties = {
        position: 'relative',
        width: healthGoal !== 'maintainWeight' ? '30%' : '45%'
    };

    const updateCurrentWeight = (event: any) => {
        setCurrentWeight(event.target.value);
    };

    const updateTargetWeight = (event: any) => {
        setTargetWeight(event.target.value);
    };

    const updateHeight = (event: any) => {
        setHeight(event.target.value);
    };

    useEffect(() => {
        if (typeof currentWeight === 'string' && currentWeight !== '' && typeof height === 'string' && height !== '') {
            const bmi = Number(currentWeight) / (Number(height) * Number(height));
            setBMI(bmi);
            return;
        }
        setBMI(null);
    }, [currentWeight, height])

    return (
        <div className='physics__container'>
            <div className='components__holder' style={physicsContainerStyles}>
                <div className='weight' style={physicsDivsStyles}>
                    <input id='weight' type='number' style={physicsInputStyles} value={typeof currentWeight === 'string' && currentWeight !== '' ? currentWeight : ''} onChange={updateCurrentWeight} />
                    <label htmlFor="weight" style={labelStyles}>Your Current Weight in kg</label>
                    <small style={smallTextStyles}>required</small>
                </div>
                {healthGoal !== 'maintainWeight' && <div className='target-weight' style={physicsDivsStyles}>
                    <input id='target-weight' type='number' style={physicsInputStyles} value={typeof targetWeight === 'string' && targetWeight !== '' ? targetWeight : ''} onChange={updateTargetWeight} />
                    <label htmlFor="target-weight" style={labelStyles}>Your Target Weight in kg</label>
                    <small style={smallTextStyles}>required</small>
                </div>}
                <div className='height' style={physicsDivsStyles}>
                    <input id='height' type='number' style={physicsInputStyles} value={typeof height === 'string' && height !== '' ? height : ''} onChange={updateHeight} />
                    <label htmlFor="height" style={labelStyles}>Your Height in cm</label>
                    <small style={smallTextStyles}>required</small>
                </div>
            </div>
            <div className='bmi__holder' style={bmiHolderDivStyle}>
                <input id='bmi' type='number' readOnly style={BMIInputStyles} value={typeof BMI === 'number' ? BMI : ''} />
                <label htmlFor='bmi' style={labelStyles}>Your BMI</label>
            </div>
        </div>
    )
};

export default Physics;