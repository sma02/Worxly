import { useEffect } from 'react';
import axios from 'axios';

function Login() {

    useEffect(() => {
        getUsers();
    }, []);
  return (
    <p>Hello world!</p>
    );

    async function getUsers() {
        const users = await axios.get('api/User');
        console.log(users.data);
    }
}

export default Login;