# Jago-System
## Introduction
It's a role-based application that manages passengers and trips, each passenger can have multiple trips.

### Role-based rules:
<ul>
    <li>User can add passengers and trips (CR);</li>
    <li>Admin can operate all the CRUD operations successfully;</li>
    <li>Admin can register a new account and choose what type of role for another user;</li>
    <li>There should be only User and Admin for this application;</li>

</ul>

<h2> Features</h2>
<h3>Login</h3>

![login-page](https://github.com/user-attachments/assets/e7df72ef-433c-4650-b450-d980c6354a1a)

<h3>Home</h3>

![home](https://github.com/user-attachments/assets/887ce67a-cfe2-42e3-8af2-063c2727fba6)

<h3>Registration</h3>

![register-page](https://github.com/user-attachments/assets/c8cb2122-8a82-4787-96cc-85cfd8a8d1c0)

<h3>Passengers page for Admin user</h3>

![pax-page](https://github.com/user-attachments/assets/1f58236f-b803-4223-8058-638925592b87)

<h3>Passengers page for User user</h3>

![pax-page-users](https://github.com/user-attachments/assets/e8d6aad1-4f21-4b45-823d-115e22fd4626)

<h3>New trip page</h3>

![trip-add-page-users](https://github.com/user-attachments/assets/652815f4-6612-4d79-9f90-7712d4b44b98)

<h1>Continuous Integration Pipeline</h1>

<h2>Purpose</h2>
<p>This pipeline is designed for practicing CI/CD by automatically building and testing the project using xUnit. It triggers on pushes to the <code>main</code> branch and runs a series of steps to ensure the code is successfully built and passes all unit tests.</p>

<h2>Trigger</h2>
<ul>
    <li><strong>Push</strong>: The pipeline runs whenever there is a push to the <code>main</code> branch.</li>
</ul>

<h2>Environment Variables</h2>
<ul>
    <li><strong>DOTNET_VERSION</strong>: Set to <code>8.0.x</code>, specifying the .NET version used in the pipeline.</li>
</ul>

<h2>Pipeline Stages</h2>
<ol>
    <li><strong>Checkout Code</strong>
        <ul>
            <li><strong>Action</strong>: Uses <code>actions/checkout@v4</code> to pull the latest code from the <code>main</code> branch.</li>
        </ul>
    </li>
    <li><strong>Setup .NET</strong>
        <ul>
            <li><strong>Action</strong>: Uses <code>actions/setup-dotnet@v4</code> to install .NET SDK version specified by <code>DOTNET_VERSION</code>.</li>
        </ul>
    </li>
    <li><strong>Install Dependencies</strong>
        <ul>
            <li><strong>Command</strong>: <code>dotnet restore</code> installs all project dependencies.</li>
        </ul>
    </li>
    <li><strong>Build Project</strong>
        <ul>
            <li><strong>Command</strong>: <code>dotnet build --configuration Release --no-restore</code> builds the code in Release mode. The <code>--no-restore</code> flag skips the dependency restore step, as it has already been completed.</li>
        </ul>
    </li>
    <li><strong>Run Unit Tests</strong>
        <ul>
            <li><strong>Command</strong>: <code>dotnet test --configuration Release --no-build</code> runs the tests in Release mode, using xUnit as the test framework. The <code>--no-build</code> flag skips the build step, as it has already been completed.</li>
        </ul>
    </li>
</ol>
