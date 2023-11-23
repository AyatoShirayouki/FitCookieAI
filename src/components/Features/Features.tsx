import React from 'react';
import FeatureCard from './FeatureCard';

interface FeatureCardInterface {
    id: number,
    iconClass: string,
    header: string,
    paragraph: string,
};

const featuresStyles: React.CSSProperties = {
    textAlign: 'center',
    marginBottom: 100,
};

const featuresHeaderStyles: React.CSSProperties = {
    margin: 0,
    fontSize: 20,
    fontWeight: 500,
    color: '#fbbc34',
};

const headerStyles: React.CSSProperties = {
    marginTop: 8,
    marginBottom: '3rem',
    fontSize: 'calc(1.375rem + 1.5vw)',
    color: '#262B47',
};

const featuresContainerStyles: React.CSSProperties = {
    display: 'flex',
    justifyContent: 'space-between',
    gap: 15,
};

const featureCards: FeatureCardInterface[] = [
    {
        id: 1,
        iconClass: 'fa fa-eye text-white fs-4',
        header: 'Highly personalized',
        paragraph: `A highly personalized diet plan is tailored to meet an individual's unique needs and preferences.`
    },
    {
        id: 2,
        iconClass: 'fa fa-bars text-white fs-4',
        header: 'Easy to use',
        paragraph: 'Share your age, height, weight, activity, dietary needs — Our AI creates perfect nutrition for you!',
    },
    {
        id: 3,
        iconClass: 'fa fa-edit text-white fs-4',
        header: 'Powered by AI',
        paragraph: 'Our highly personalized diet plan, powered by AI, is revolutionizing the way people approach nutrition.',
    },
];

const Features = ({ }) => {

    const printFeatureCard = () => {
        return featureCards.map((card: FeatureCardInterface) => {
            const { id, iconClass, header, paragraph } = card;
            return <FeatureCard key={id} iconClass={iconClass} header={header} paragraph={paragraph} />
        })
    };

    return (
        <section id='features' className='features' style={featuresStyles}>
            <h5 style={featuresHeaderStyles}>App Features</h5>
            <h1 style={headerStyles}>Awesome Features</h1>
            <div className='feature-cards-container' style={featuresContainerStyles}>
                {printFeatureCard()}
            </div>
        </section>
    )
};

export default Features;