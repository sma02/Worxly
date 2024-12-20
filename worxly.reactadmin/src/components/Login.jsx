import {useEffect} from 'react';

function Login() {

    useEffect(() => {
        getUsers();
    }, []);
  return (
    <p>Hello world!</p>
    );

    async function getUsers() {
        const res = await fetch('api/User');
        const users = await res?.json();
        console.log(users);
    }
}

export default Login;