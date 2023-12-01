import React, { useEffect, useState, useRef } from 'react';
import HealthGoal from '../HealthGoal/HealthGoal';
import Physics from '../Physics/Physics';
import OccupationActivity from '../OccupationActivity/OccupationActivity';
import RestrictionsPreferences from '../RestrictionsPreferences/RestrictionsPreferences';
import DietPlanQuestionSquare from '../DietPlanQuestionSquare/DietPlanQuestionSquare';
import Form from '../Form/Form';
import DietPlan from '../DietPlan/DietPlan';
import axios from 'axios';
import usePdfExport from '../../hooks/usePdfExport';
import generatePDF, { Resolution, Margin } from 'react-to-pdf';
import { useReactToPrint } from 'react-to-print';

interface DietPlanGeneratorProps {
    isLoggedIn: boolean,
    setIsLoggedIn: Function,
    loginResponse: any,
    setLoginResponse: Function
};

const dietPlanGeneratorStyles: React.CSSProperties = {
    textAlign: 'center',
    marginBottom: 100,
    padding: '0px 200px'
};

const dietPlanGeneratorHeaderStyles: React.CSSProperties = {
    margin: 0,
    fontSize: 20,
    fontWeight: 500,
    color: '#fbbc34',
};

const headerStyles: React.CSSProperties = {
    marginTop: 8,
    marginBottom: '4rem',
    fontSize: 'calc(1.375rem + 1.5vw)',
    color: '#262B47',
};

const dietPlanNavigationButtonStyles: React.CSSProperties = {
    display: 'block',
    marginTop: 9,
    padding: '1rem 2rem',
    borderRadius: '50rem',
    color: '#fff',
    background: 'linear-gradient(to bottom right, #974700, #fbbc34)',
    textDecoration: 'none',
    outline: 'none',
    cursor: 'pointer'
};

const dietPlanNavigationDisabledButtonStyles: React.CSSProperties = {
    display: 'block',
    marginTop: 9,
    padding: '1rem 2rem',
    borderRadius: '50rem',
    color: '#fff',
    background: 'rgb(247, 181, 120)',
    textDecoration: 'none',
    outline: 'none',
    pointerEvents: 'none',
    cursor: 'default',
};

const dietPlanQuestionSquaresContainerStyles: React.CSSProperties = {
    display: 'flex',
    justifyContent: 'space-evenly',
    marginBottom: 25,
};

const paragraphStyles: React.CSSProperties = {
    color: '#919294',
};

const gradientButtontyles: React.CSSProperties = {
    display: 'inline-block',
    margin: 'auto 0',
    padding: '0.5rem 1.5rem',
    borderRadius: '50rem',
    color: '#fff',
    background: 'linear-gradient(to bottom right, #974700, #fbbc34)',
    textDecoration: 'none',
    outline: 'none',
    cursor: 'pointer'
};

export const removeWhiteSpaces = (input: string) => {
    return input.replace(/^\s+|\s+$/g, '');
};

export const validateInput = (input: string, limit: number) => {
    return input.replace(/^\s+|\s+$/g, '').length >= limit;
};

export const validatePhoneNumber = (phoneNumber: string) => {
    const phoneRegex = /^\+?\d{1,20}$/;
    return phoneRegex.test(phoneNumber);
};

export const validEmailReg = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

const initSignUpState = { Email: '', Password: '', confirmPassword: '', FirstName: '', LastName: '', Gender: 'male', DOB: null, PhoneNumber: '', DOJ: new Date() };

const DietPlanGenerator = ({ isLoggedIn, setIsLoggedIn, loginResponse, setLoginResponse }: DietPlanGeneratorProps) => {
    const [currentQuestion, setCurrentQuestion] = useState<any>(1);
    const [healthGoal, setHealthGoal] = useState('loseWeight');
    const [currentWeight, setCurrentWeight] = useState(null);
    const [targetWeight, setTargetWeight] = useState(null);
    const [height, setHeight] = useState(null);
    const [occupation, setOccupation] = useState('');
    const [activityLevel, setActivityLevel] = useState('sedentary');
    const [dietaryRestriction, setDietaryRestrictions] = useState('');
    const [foodPreferences, setFoodPreferences] = useState('');
    const [BMI, setBMI] = useState(null);
    const [loginData, setLoginData] = useState({ email: '', password: '' });
    const [signUpData, setSignUpData] = useState({ ...initSignUpState });
    const [isRegistered, setIsRegistered] = useState<boolean>(true);
    const [termsAndConditionsAccepted, setTermsAndConditionsAccepted] = useState<boolean>(false);
    const [tokens, setTokens] = useState<{ token: string, refreshtoken: string }>({ token: '', refreshtoken: '' });
    const [tables, setTables] = useState<{ dietPlan: string | null, supplements: string | null }>({ dietPlan: null, supplements: null });
    const [shoppingBasket, setShoppingBasket] = useState<string | null>(null);
    const [isGenerating, setIsGenerating] = useState<boolean>(false);
    const [loginError, setLoginError] = useState<any>(null);
    const [signUpError, setSignUpError] = useState<boolean>(false);
    const printComponent = useRef(null);


    const navigationButtonStyleQ2 = healthGoal !== 'maintainWeight' ? !currentWeight || !targetWeight || !height ? dietPlanNavigationDisabledButtonStyles : dietPlanNavigationButtonStyles : !currentWeight || !height ? dietPlanNavigationDisabledButtonStyles : dietPlanNavigationButtonStyles;
    const navigationButtonStyleQ3 = occupation === '' ? dietPlanNavigationDisabledButtonStyles : dietPlanNavigationButtonStyles;

    const dietPlanProperties = [
        {
            id: 1,
            options: [
                {
                    option_id: 1,
                    text: 'Lose Weight',
                    value: 'loseWeight',
                },
                {
                    option_id: 2,
                    text: 'Gain Weight',
                    value: 'gainWeight',
                },
                {
                    option_id: 3,
                    text: 'Maintain Weight',
                    value: 'maintainWeight',
                },
            ],
            component: function () {
                return <HealthGoal key={this.id} options={this.options} healthGoal={healthGoal} setHealthGoal={setHealthGoal} />
            }
        },
        {
            id: 2,
            component: function () {
                return <Physics key={this.id} healthGoal={healthGoal} currentWeight={currentWeight} setCurrentWeight={setCurrentWeight} targetWeight={targetWeight} setTargetWeight={setTargetWeight} height={height} setHeight={setHeight} BMI={BMI} setBMI={setBMI} />
            }
        },
        {
            id: 3,
            options: [
                {
                    option_id: 1,
                    text: 'Sedentary',
                    value: 'sedentary'
                },
                {
                    option_id: 2,
                    text: 'Lightly Active',
                    value: 'lightlyActive'
                },
                {
                    option_id: 3,
                    text: 'Moderately Active',
                    value: 'moderatelyActive'
                },
                {
                    option_id: 4,
                    text: 'Very Active',
                    value: 'veryActive'
                },
                {
                    option_id: 5,
                    text: 'Extremely Active',
                    value: 'extremelyActive'
                }
            ],
            component: function () {
                return <OccupationActivity key={this.id} options={this.options} occupation={occupation} setOccupation={setOccupation} activityLevel={activityLevel} setActivityLevel={setActivityLevel} />
            }
        },
        {
            id: 4,
            component: function () {
                return <RestrictionsPreferences key={this.id} dietaryRestriction={dietaryRestriction} setDietaryRestrictions={setDietaryRestrictions} foodPreferences={foodPreferences} setFoodPreferences={setFoodPreferences} />
            }
        },
        {
            id: 5,
            component: function () {
                return !isLoggedIn ? <Form isRegistered={isRegistered} setIsRegistered={setIsRegistered} loginData={loginData} setLoginData={setLoginData} signUpData={signUpData} setSignUpData={setSignUpData} termsAndConditionsAccepted={termsAndConditionsAccepted} setTermsAndConditionsAccepted={setTermsAndConditionsAccepted} signUpError={signUpError} /> : <DietPlan loginResponse={loginResponse} />
            }
        }
    ];

    const getUserYears = (DOB: Date | string) => {
        const dob = new Date(DOB);

        const now = new Date();
        const ageInMilliseconds = now.getTime() - dob.getTime();

        const ageInYears = Math.floor(ageInMilliseconds / (365.25 * 24 * 60 * 60 * 1000));
        return ageInYears;
    };

    const handleDietPlanGeneratorNavigation = (type: boolean) => {
        if (type) { setCurrentQuestion((question: number) => ++question); return; }
        setCurrentQuestion((question: number) => --question);
    };

    const extractQuestionSquares = () => {
        return dietPlanProperties.map((_: any, idx: number) => <DietPlanQuestionSquare key={idx + 1} question={idx + 1} currentQuestion={currentQuestion} />)
    };

    const fetchSignUpData = async (data: any) => {
        const dataCopy = { ...data };
        delete dataCopy.confirmPassword;
        try {
            const response = await fetch("http://fitcookieai.uksouth.cloudapp.azure.com:8087/api/Users/SignUp", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(dataCopy),
            });
            const result = await response.json();
            const { code, error, body: resBody } = result;
            if (code === 201) {
                const body = await loginUser(dataCopy.Email, dataCopy.Password);
                setLoginResponse({ body });
                setIsLoggedIn(true);
                setIsGenerating(false);
                if (loginError) setLoginError(null)
            }
            else if (code === 200) { setIsLoggedIn(false); setLoginError({ body: resBody }); }
        } catch (error) {
            alert(error);
            console.error("Error:", error);
        }
    };

    const loginUser = async (email?: string, password?: string) => {
        // localStorage.setItem('email', JSON.stringify(!email && !password ? loginData.email : email));
        // localStorage.setItem('password', JSON.stringify(!email && !password ? loginData.password : password));
        // const response = await fetch(!email && !password ? `https://localhost:7002/api/Users/Login?email=${loginData.email}&password=${loginData.password}` : `https://localhost:7002/api/Users/Login?email=${email}&password=${password}`);
        setIsGenerating(true);
        const response = await axios.get(!email && !password ? `http://fitcookieai.uksouth.cloudapp.azure.com:8087/api/Users/Login?email=${loginData.email}&password=${loginData.password}` : `http://fitcookieai.uksouth.cloudapp.azure.com:8087/api/Users/Login?email=${email}&password=${password}`);
        const { data: { body, code, error }, headers: { token, refreshtoken } } = response;
        if (code === 201) setTokens({ token, refreshtoken });
        if (email && password) return body;
        if (code === 201) {
            setLoginResponse({ body });
            setIsLoggedIn(true);
            if (loginError) setLoginError(null)
        } else if (code === 200) {
            setIsLoggedIn(false);
            setLoginError({ body: error })
        }
        setIsGenerating(false);
    };

    const signUp = async () => {
        if (signUpData.Email !== '' && signUpData.Email.match(validEmailReg) && validateInput(signUpData.FirstName, 1) && validateInput(signUpData.LastName, 1) && validateInput(signUpData.Password, 6) && signUpData.Password === signUpData.confirmPassword && signUpData.DOB !== '' && validatePhoneNumber(signUpData.PhoneNumber) && termsAndConditionsAccepted) {
            await fetchSignUpData({ ...signUpData });
            setIsRegistered(true);
            setSignUpData({ ...initSignUpState });
            if (signUpError) setSignUpError(false);
            return;
        }
        setSignUpData({ ...signUpData });
        setSignUpError(true);
    };

    useEffect(() => {
        if (!isLoggedIn && tables.dietPlan && tables.supplements) {
            setTables({ dietPlan: null, supplements: null });
            setTokens({ token: '', refreshtoken: '' });
        }
    }, [isLoggedIn])

    const getDietPlanInput = () => {
        const { body: { dob, gender } } = loginResponse;
        const years = getUserYears(dob);
        return `As a profesional dietitian recommend me a diverse diet plan with a large variety of delicious meals meals which must be different for every day of the week, I am a ${gender}, ${years} years old, I weigh ${currentWeight} kilograms, my target weight is ${targetWeight} kilograms, my height is ${height} meters, my BMI is ${BMI}, my activity level is ${activityLevel}${validateInput(dietaryRestriction, 1) ? `, My dietary restrictions are ${dietaryRestriction}` : ``} ${validateInput(foodPreferences, 1) ? `, My food preferences are ${foodPreferences}` : ``}, my healt goal is to ${healthGoal}, and my oocupations is ${occupation}. Return the result as a HTML table with id=diet_plan_table in html format with colums: day of the week and meals(here you should include the quantity of each part of the meal in grams)`
    };

    const getSupplementsInput = () => {
        const { body: { dob, gender } } = loginResponse;
        const years = getUserYears(dob);
        return `As a profesional dietitian recomend me a list of supplements, how much of them should i take per day and briefly explain the benefits of each of them,
        (strenght building and health related) which would help me based on my needs in html format as a html table with id=supplements_table and with collumns Supplement, Description and Dose per day.
        I am a ${gender}, ${years} years old, I weight ${currentWeight} kilograms, my target weight is ${targetWeight} kilograms, my height is ${height} meters, my BMI is ${BMI}, my activity level is ${activityLevel}${validateInput(dietaryRestriction, 1) ? `, My dietary restrictions are ${dietaryRestriction}` : ``} ${validateInput(foodPreferences, 1) ? `, My food preferences are ${foodPreferences}` : ``}, my healt goal is to ${healthGoal}, and my oocupations is ${occupation}.`;
    };

    const generateDietAndSupplementPlans = async () => {
        setIsGenerating(true);
        try {
            const responseDietPlan = await axios.get(`http://fitcookieai.uksouth.cloudapp.azure.com:8088/api/GPT/GetGPTResponse?input=${getDietPlanInput()}`,
                {
                    headers: {
                        "Content-Type": "application/json",
                        "token": tokens.token,
                        "refreshtoken": tokens.refreshtoken
                    }
                });

            const { data: { choices: dietChoices }, headers: { token, refreshtoken } } = responseDietPlan;


            const responseSupplementPlan = await axios.get(`http://fitcookieai.uksouth.cloudapp.azure.com:8088/api/GPT/GetGPTResponse?input=${getSupplementsInput()}`, {
                headers: {
                    "Content-Type": "application/json",
                    "token": token,
                    "refreshtoken": refreshtoken
                }
            });
            const { data: { choices: supplementsChoices }, headers: { token: newToken, refreshtoken: newRefreshToken } } = responseSupplementPlan;

            const tablesCopy = { ...tables };
            tablesCopy.dietPlan = dietChoices[0].text
            tablesCopy.supplements = supplementsChoices[0].text;
            setTables(tablesCopy);
            setTokens({ token: newToken, refreshtoken: newRefreshToken });
        } catch (error) {
            setTables({ dietPlan: null, supplements: null })
            console.error("Error:", error);
        }
        setIsGenerating(false);
    };

    const { exportToPDF } = usePdfExport('plansWrapper', {
        margin: 10,
        filename: 'exported-document.pdf',
        image: { type: 'jpeg', quality: 0.98 },
        html2canvas: { scale: 2 },
        jsPDF: { unit: 'mm', format: 'a4', orientation: 'portrait' },
    });

    const handlePrint = useReactToPrint({
        content: () => printComponent.current,
        documentTitle: 'Plans'
    });

    const extractUniqueIngredients = () => {
        if (isLoggedIn && !isGenerating && tables.dietPlan && tables.supplements) {
            const uniqueIngredients: string[] = [];
            const dietPlanTable = document.getElementById("diet_plan_table");
            const rows = dietPlanTable!.querySelectorAll("tbody tr");
            for (let i = 1; i < rows.length; i++) {
                const row = rows[i];
                const mealText = row.querySelector("td:nth-child(2)")!.textContent;
                console.log(mealText);
                const regex = /(\d+\s?\w+\s(?:of\s)?(?:[a-zA-Z]+\s?)*)/g;
                const matches = mealText?.match(regex);
                if (matches) matches.forEach((match) => { if (!uniqueIngredients.includes(match)) uniqueIngredients.push(match) })
            }
            return `Please examine the following list of ingredients: ${uniqueIngredients.join(", ")}. For each ingredient, identify the fundamental grocery item that needs to be purchased from a supermarket. Please exclude any preparation methods (like 'steamed', 'diced', 'grilled' etc.) and convert the quantities to either grams or kilograms. Present the results in an HTML table with an id of 'shopping_basket_table', containing columns for 'Product' and 'Quantity'.`;
        }
    };

    const generateShoppingBasket = async () => {
        setIsGenerating(true);
        try {
            const responseDietPlan = await axios.get(`http://fitcookieai.uksouth.cloudapp.azure.com:8088/api/GPT/GetGPTResponse?input=${extractUniqueIngredients()}`,
                {
                    headers: {
                        "Content-Type": "application/json",
                        "token": tokens.token,
                        "refreshtoken": tokens.refreshtoken
                    }
                });

            const { data: { choices: dietChoices }, headers: { token, refreshtoken } } = responseDietPlan;

            setShoppingBasket(dietChoices[0].text);
            setTokens({ token, refreshtoken });
        } catch (error) {
            console.error("Error:", error);
        }
        setIsGenerating(false);
    };

    const handlePrintTables = (e: any) => {
        e.preventDefault();

        // Create a copy of the target div
        var resultContainer = document.getElementById('plansWrapper');
        var printContent = resultContainer!.cloneNode(true);

        // Create a new window
        var printWindow = window.open('', '', 'width=800,height=800');

        // Write the modified HTML to the new window
        printWindow!.document.write('<html><head><title>Diet Plan</title></head><body>');
        printWindow!.document.write((printContent as HTMLElement).innerHTML);
        printWindow!.document.write('</body></html>');
        printWindow!.document.close();
        printWindow!.print();
    };

    return (
        <section id='dietPlanGenerator' className='diet-plan-generator' style={dietPlanGeneratorStyles}>
            <h5 style={dietPlanGeneratorHeaderStyles}>Diet plan Generator</h5>
            <h1 style={headerStyles}>Generate a diet plan!</h1>
            <p style={paragraphStyles}>Hello! I'm here to help you with your nutrition needs. To get started, please provide some information about yourself.</p>
            <div className='question-squares__container' style={dietPlanQuestionSquaresContainerStyles}>{extractQuestionSquares()}</div>
            <div style={{ marginBottom: 35 }}>{dietPlanProperties.map((val: any) => val.id === currentQuestion && val.component())}</div>
            {currentQuestion === 5 && !isGenerating && isRegistered && loginError && loginError.body && <p style={{ textAlign: 'left', color: '#ff1212' }}>{loginError.body}</p>}
            {currentQuestion < dietPlanProperties.length && <a className='gradient-btn next-btn' style={currentQuestion === 2 ? navigationButtonStyleQ2 : currentQuestion === 3 ? navigationButtonStyleQ3 : dietPlanNavigationButtonStyles} onClick={() => { handleDietPlanGeneratorNavigation(true) }}>Next</a>}
            {!isLoggedIn && currentQuestion === 5 && !isGenerating && <a className='gradient-btn next-btn' style={dietPlanNavigationButtonStyles} onClick={async () => {
                if (!isRegistered) await signUp();
                else await loginUser();
            }}>{isRegistered ? 'Login' : 'Sign Up'}</a>}
            {isLoggedIn && currentQuestion === 5 && !isGenerating && <a className='gradient-btn previous-btn' style={dietPlanNavigationButtonStyles} onClick={async () => { await generateDietAndSupplementPlans() }}>Generate</a>}
            {currentQuestion > 1 && !isGenerating && <a className='gradient-btn previous-btn' style={{ ...dietPlanNavigationButtonStyles, marginBottom: isLoggedIn && !isGenerating && tables.dietPlan && tables.supplements ? 50 : 0 }} onClick={() => { handleDietPlanGeneratorNavigation(false) }}>Previous</a>}
            {isGenerating && <div className='loading-cookie__holder'>
                <img src='https://i.ibb.co/Ny4Gr4f/fitcookieloading-gif.gif' alt='loading-cookie' />
            </div>}
            {isLoggedIn && !isGenerating && tables.dietPlan && tables.supplements &&
                <>
                    <section id='plansWrapper' className='plans-wrapper' ref={printComponent}>
                        <div>
                            <h5 style={dietPlanGeneratorHeaderStyles}>Plan</h5>
                            <h1 style={headerStyles}>Your personalized plan!</h1>
                            <div className='diet-plan-table-wrapper' dangerouslySetInnerHTML={{ __html: `${tables.dietPlan}` }}>
                            </div>
                        </div>
                        <div style={{ pageBreakBefore: 'always' }}>
                        </div>
                        <div>
                            <h5 style={dietPlanGeneratorHeaderStyles}>Supplements</h5>
                            <h1 style={headerStyles}>Your recommended supplements!</h1>
                            <div className='supplements-plan-table-wrapper' dangerouslySetInnerHTML={{ __html: `${tables.supplements}` }}>
                            </div>
                        </div>
                        {shoppingBasket && <>
                            <div style={{ pageBreakBefore: 'always' }}>
                            </div>
                            <div>
                                <h5 style={dietPlanGeneratorHeaderStyles}>Shopping basket</h5>
                                <h1 style={headerStyles}>Your recommended shopping basket!</h1>
                                <div className='shopping-basket-table-wrapper' dangerouslySetInnerHTML={{ __html: `${shoppingBasket}` }}>
                                </div>
                            </div>
                        </>}
                    </section>
                    <div style={{ display: 'flex', flexDirection: 'column', gap: 15 }}>
                        <a className='gradient-btn pdf-button' style={{ ...gradientButtontyles, alignSelf: 'center' }} onClick={async () => { await generateShoppingBasket() }}>Generate shopping basket</a>
                        <a className='gradient-btn pdf-button' style={{ ...gradientButtontyles, alignSelf: 'center' }} onClick={exportToPDF}>Download as pdf</a>
                        <a className='gradient-btn pdf-button' style={{ ...gradientButtontyles, alignSelf: 'center' }} onClick={handlePrintTables}>Print</a>
                    </div>
                </>
            }
        </section>
    )
};

export default DietPlanGenerator;