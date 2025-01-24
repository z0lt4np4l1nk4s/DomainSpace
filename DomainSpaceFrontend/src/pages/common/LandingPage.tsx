import { useEffect, useState } from "react";
import { FooterComponent, Loader, NavBar } from "../../components";
import { JokeModel, PageRouteEnum } from "../../types";
import { JokeService } from "../../services";

const jokeService = new JokeService();

export default function LandingPage() {
  const [joke, setJoke] = useState<JokeModel>({});
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const getJokeAsync = async () => {
      const response = await jokeService.getAsync();

      if (response.isSuccess) {
        setJoke(response.result!);
      }

      setIsLoading(false);
    };

    getJokeAsync();
  }, []);

  return (
    <div className="d-flex flex-column vh-100">
      <div className="flex-grow-1">
        <NavBar />
        <Loader isLoading={isLoading}>
          <header className="jumbotron jumbotron-fluid bg-primary text-white text-center py-5">
            <div className="container">
              <h1 className="display-4 fw-bold">
                Share Files Securely with Your Domain
              </h1>
              <p className="lead mb-4">
                With DomainSpace, share files and scripts only with users who
                share your email domain.
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
              <h2 className="text-center mb-5 fw-bold">How It Works</h2>
              <div className="row g-4">
                <div className="col-md-4">
                  <div className="card shadow-lg rounded-lg bg-primary">
                    <div className="card-body">
                      <h5 className="card-title fw-bold">Step 1: Sign Up</h5>
                      <p className="card-text">
                        Sign up using your email address that matches your
                        domain.
                      </p>
                    </div>
                    <div className="card-footer bg-primary text-center">
                      <i
                        className="bi bi-person-circle text-white"
                        style={{ fontSize: "2rem" }}
                      ></i>
                    </div>
                  </div>
                </div>

                <div className="col-md-4">
                  <div className="card h-100 shadow-lg rounded-lg bg-primary">
                    <div className="card-body">
                      <h5 className="card-title fw-bold">
                        Step 2: Upload Files
                      </h5>
                      <p className="card-text">
                        Upload files or scripts you want to share securely.
                      </p>
                    </div>
                    {joke.joke ? (
                      <div className="card-footer bg-primary text-dark">
                        <div
                          className="text-dark bg-white rounded border position-relative border-dark"
                          style={{
                            fontFamily: "monospace",
                            whiteSpace: "pre-wrap",
                            wordWrap: "break-word",
                          }}
                        >
                          <div
                            className="bg-secondary text-white px-2 py-1"
                            style={{
                              fontSize: "0.85rem",
                              fontWeight: "bold",
                              width: "100%",
                              borderTopLeftRadius: "0.25rem",
                              borderTopRightRadius: "0.25rem",
                            }}
                          >
                            joke.txt
                          </div>

                          <div className="p-3">{joke.joke}</div>
                        </div>
                      </div>
                    ) : (
                      <div className="card-footer bg-primary text-center">
                        <i
                          className="bi bi-upload text-white"
                          style={{ fontSize: "2rem" }}
                        ></i>
                      </div>
                    )}
                  </div>
                </div>

                <div className="col-md-4">
                  <div className="card shadow-lg rounded-lg bg-primary">
                    <div className="card-body">
                      <h5 className="card-title fw-bold">Step 3: Share</h5>
                      <p className="card-text">
                        Share your content with others who have matching email
                        addresses.
                      </p>
                    </div>
                    <div className="card-footer bg-primary text-center">
                      <i
                        className="bi bi-share text-white"
                        style={{ fontSize: "2rem" }}
                      ></i>
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
                      <h5 className="card-title fw-bold">
                        Domain-Based Privacy
                      </h5>
                      <p className="card-text">
                        Only users with the same email domain can access your
                        files.
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
                        Your data is safe, as sharing is restricted to trusted
                        users within your domain.
                      </p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </section>
        </Loader>
      </div>
      <FooterComponent />
    </div>
  );
}
