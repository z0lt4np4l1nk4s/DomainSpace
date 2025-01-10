import { FooterComponent, NavBar } from "../../components";
import { PageRouteEnum } from "../../types";

export default function LandingPage() {
  return (
    <div>
      <NavBar />

      <header className="jumbotron jumbotron-fluid bg-primary text-white text-center py-5">
        <div className="container">
          <h1 className="display-4 fw-bold">
            Share Files Securely with Your Domain
          </h1>
          <p className="lead mb-4">
            With DomainSpace, share files and scripts only with users who share
            your email domain.
          </p>
          <a
            href={PageRouteEnum.Login}
            className="btn btn-dark btn-lg px-5 py-3 shadow-lg"
          >
            Get Started
          </a>
        </div>
      </header>

      <section id="how-it-works" className="py-5 bg-light text-dark">
        <div className="container">
          <h2 className="text-center mb-5">How It Works</h2>
          <div className="row g-4">
            <div className="col-md-4">
              <div className="card h-100 shadow-lg border-light rounded-lg">
                <div className="card-body">
                  <h5 className="card-title fw-bold">Step 1: Sign Up</h5>
                  <p className="card-text">
                    Sign up using your email address that matches your domain.
                  </p>
                </div>
              </div>
            </div>
            <div className="col-md-4">
              <div className="card h-100 shadow-lg border-light rounded-lg">
                <div className="card-body">
                  <h5 className="card-title fw-bold">Step 2: Upload Files</h5>
                  <p className="card-text">
                    Upload files or scripts you want to share securely.
                  </p>
                </div>
              </div>
            </div>
            <div className="col-md-4">
              <div className="card h-100 shadow-lg border-light rounded-lg">
                <div className="card-body">
                  <h5 className="card-title fw-bold">Step 3: Share</h5>
                  <p className="card-text">
                    Share your content with others who have matching email
                    addresses.
                  </p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>

      <section id="features" className="bg-secondary text-white py-5">
        <div className="container">
          <h2 className="text-center mb-5">Key Features</h2>
          <div className="row g-4">
            <div className="col-md-4">
              <div className="card h-100 shadow-sm border-light rounded-lg">
                <div className="card-body">
                  <h5 className="card-title fw-bold">Domain-Based Privacy</h5>
                  <p className="card-text">
                    Only users with the same email domain can access your files.
                  </p>
                </div>
              </div>
            </div>
            <div className="col-md-4">
              <div className="card h-100 shadow-sm border-light rounded-lg">
                <div className="card-body">
                  <h5 className="card-title fw-bold">Easy Sharing</h5>
                  <p className="card-text">
                    Share files with just a few clicks and control access
                    effortlessly.
                  </p>
                </div>
              </div>
            </div>
            <div className="col-md-4">
              <div className="card h-100 shadow-sm border-light rounded-lg">
                <div className="card-body">
                  <h5 className="card-title fw-bold">Secure & Private</h5>
                  <p className="card-text">
                    Your data is safe, as sharing is restricted to trusted users
                    within your domain.
                  </p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>

      <FooterComponent />
    </div>
  );
}
