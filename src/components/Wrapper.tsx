import React from 'react';

interface WrapperProps {
    children: any;
};

const wrapperStyles: React.CSSProperties = {
    width: '100%',
    height: 'auto',
    paddingLeft: 100,
    paddingRight: 100,
    boxSizing: 'border-box',
};

const Wrapper = ({ children }: WrapperProps) => {
    return (
        <section style={wrapperStyles} className="content-wrapper">
            {children}
        </section>
    )
};

export default Wrapper;