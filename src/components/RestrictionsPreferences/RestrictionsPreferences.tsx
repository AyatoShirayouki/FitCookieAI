import React from 'react';

interface RestrictionsPreferencesProps {
    dietaryRestriction: string,
    setDietaryRestrictions: Function,
    foodPreferences: string,
    setFoodPreferences: Function
};

const restrictionsPreferencesContainer: React.CSSProperties = {
    display: 'flex',
    justifyContent: 'space-between',
};

const restrictionsPreferencesyDivsStyles: React.CSSProperties = {
    position: 'relative',
    width: '45%',
};
const textareaStyles: React.CSSProperties = {
    display: 'block',
    width: '100%',
    padding: '1.9rem 1.05rem 0.75rem 1.05rem ',
    fontSize: '1rem',
    fontWeight: 400,
    lineHeight: 1.5,
    color: '#919294',
    border: '1px solid #ced4da',
    borderRadius: 10,
    boxSizing: 'border-box',
    resize: 'none'
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

const RestrictionsPreferences = ({ dietaryRestriction, setDietaryRestrictions, foodPreferences, setFoodPreferences }: RestrictionsPreferencesProps) => {
    const handleDietaryRestrictions = (event: any) => {
        setDietaryRestrictions(event.target.value);
    };

    const handleFoodPreferences = (event: any) => {
        setFoodPreferences(event.target.value);
    };
    
    return (
        <div className='restrictions-preferences__container' style={restrictionsPreferencesContainer}>
            <div className='restrictions' style={restrictionsPreferencesyDivsStyles}>
                <textarea id='restrictions' style={textareaStyles} value={dietaryRestriction} onChange={handleDietaryRestrictions}></textarea>
                <label htmlFor="restrictions" style={labelStyles}>Dietary Restrictions</label>
            </div>
            <div className='restrictions' style={restrictionsPreferencesyDivsStyles}>
                <textarea id='preferences' style={textareaStyles} value={foodPreferences} onChange={handleFoodPreferences}></textarea>
                <label htmlFor="preferences" style={labelStyles}>Food Preferences</label>
            </div>
        </div>
    )
};

export default RestrictionsPreferences;