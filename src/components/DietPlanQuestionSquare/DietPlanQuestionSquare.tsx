import React from 'react';

interface DietPlanQuestionSquareProps {
    question: number,
    currentQuestion: number,
};

const DietPlanQuestionSquare = ({ question, currentQuestion }: DietPlanQuestionSquareProps) => {
    const squareStyles: React.CSSProperties = {
        padding: 10,
        fontWeight: 700,
        color: '#fff',
        background: question <= currentQuestion ? '#bf5b00' : '#f7b578',
        borderRadius: 3
    };

    return (
        <div style={squareStyles}>{question}</div>
    )
};

export default DietPlanQuestionSquare;