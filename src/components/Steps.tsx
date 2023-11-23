import React from 'react';
import StepCard from './StepCard';

interface StepCardInterface {
    id: number,
    imgURL: string,
    header: string,
    paragraph: string,
};

const stepsContainerStyles: React.CSSProperties = {
    display: 'flex',
    justifyContent: 'space-between',
    gap: 15,
};

const stepCards: StepCardInterface[] = [
    {
        id: 1,
        imgURL: 'https://i.ibb.co/s9Ys1Q0/3.png',
        header: 'Generate Your Meal & Supplements Plan',
        paragraph: `Experience the advanced functionality of our AI-based Meal and Supplements Plan generator by seamlessly inputting essential information and activating the 'Next' button. Embark on a transformative journey towards enhanced well-being and increased contentment.`
    },
    {
        id: 2,
        imgURL: 'https://i.ibb.co/HrFy0n3/5.png',
        header: 'Talk to Our Experts',
        paragraph: 'Rest assured, our team of experts is readily available to provide prompt and insightful responses to any inquiries you may have. Never hesitate to seek their assistance for swift and valuable support.',
    },
    {
        id: 3,
        imgURL: 'https://i.ibb.co/w7NzqWY/4.png',
        header: 'Get Your Supplement Stack',
        paragraph: 'In the final stride towards achieving your desired transformation, optimize your journey by exploring our diverse array of premium supplements. Elevate your performance and attain your peak potential with the aid of our exceptional supplement selection.',
    },
];

const Steps = ({ }) => {

    const printStepCards = () => {
        return stepCards.map((step: StepCardInterface) => {
            const { id, imgURL, header, paragraph } = step;
            return <StepCard key={id} imgURL={imgURL} header={header} paragraph={paragraph} />
        })
    };

    return (
        <section id='steps' className='steps-container' style={stepsContainerStyles}>
            {printStepCards()}
        </section>
    )
};

export default Steps;