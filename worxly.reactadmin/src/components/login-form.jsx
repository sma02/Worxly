/* eslint-disable react/prop-types */
import { cn } from "@/lib/utils"
import { Button } from "@/components/ui/button"
import {
    Card,
    CardContent,
    CardDescription,
    CardHeader,
    CardTitle,
} from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { useState } from "react"
import axios from "axios"

export function LoginForm({
    className,
    ...props }) {
    const [values, SetValues] = useState({
        identifier: null,
        password: null,
    });
    const [error, SetError] = useState(0);
    async function handleSubmit(event) {
        event.preventDefault();
        try {
            const res = await axios.get('/api/User/Auth', { params: values });
            if (res.status === 200)
                props.setUser(values);
        }
        catch (e)
        {
            if (e.status == 404) {
                SetError("Invalid email or password");
            }
            else {
                SetError("An error occured connecting to server");
            }
            console.log(e.status);
           // alert(e);
        }
    }
    function handleChange(event) {
        const { id, value } = event.target;
        SetValues({
            ...values,
            [id]: value,
        });
    }
    return (
        <div className={cn("flex flex-col gap-6", className)} {...props}>
            <Card>
                <CardHeader>
                    <CardTitle className="text-2xl">Login</CardTitle>
                    <CardDescription>
                        Enter your email below to login to your account
                    </CardDescription>
                </CardHeader>
                <CardContent>
                    <form onSubmit={handleSubmit}>
                        <div className="flex flex-col gap-6">
                            <div className="grid gap-2">
                                <Label htmlFor="email">Email</Label>
                                <Input
                                    id="identifier"
                                    type="email"
                                    required
                                    onChangeCapture={handleChange}
                                />
                            </div>
                            <div className="grid gap-2">
                                <div className="flex items-center">
                                    <Label htmlFor="password">Password</Label>
                                    <a
                                        href="#"
                                        className="ml-auto inline-block text-sm underline-offset-4 hover:underline"
                                    >
                                        Forgot your password?
                                    </a>
                                </div>
                                <Input id="password"
                                    type="password"
                                    required
                                    onChangeCapture={handleChange} />
                            </div>
                            {error == '' ? null : 
                                <div className="bg-orange-100 border-l-2 border-orange-500 text-orange-700 p-4" role="alert">
                                    <p>{error}</p>
                                </div>}
                            <Button type="submit" className="w-full">
                                Login
                            </Button>
                        </div>
                        <div className="mt-4 text-center text-sm">
                            Don&apos;t have an account?{" "}
                            <a href="#" className="underline underline-offset-4">
                                Sign up
                            </a>
                        </div>
                    </form>
                </CardContent>
            </Card>
        </div>
    )
}
