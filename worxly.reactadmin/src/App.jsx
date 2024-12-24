import { useState, useEffect } from 'react'
import './App.css'
import { LoginForm } from './components/login-form'

function App() {
    const [count, setCount] = useState(0)
    const [user, setUser] = useState({ identifier: null, password: null });

    useEffect(() => {
        if (user.identifier != null)
            console.log(user);
            //alert('user logged in');
    }, [user]);
  return (
    <>
      <div className="card dark">
              {user.identifier ? <div>User logged in</div> : <LoginForm setUser={setUser} />}
      </div>
    </>
  )
}

export default App
