import { useEffect, useRef, useState } from "react";
import {
  FooterComponent,
  LikeButtonComponent,
  Loader,
  NavBar,
} from "../../components";
import { useParams } from "react-router-dom";
import { ContentModel, SignalRMethodsEnum } from "../../types";
import { ContentService, FileService, SignalRService } from "../../services";
import { FaDownload } from "react-icons/fa";
import { ToastUtil } from "../../utils";

const contentService = new ContentService();
const signalrService = new SignalRService();
const fileService = new FileService();

export default function ContentDetailsPage() {
  const [isLoading, setIsLoading] = useState(true);
  const [content, setContent] = useState<ContentModel>();
  const params = useParams();
  const contentRef = useRef<ContentModel | undefined>(content);

  useEffect(() => {
    contentRef.current = content;
  }, [content]);

  const handleDownloadAll = async () => {
    const response = await fileService.downloadAllFilesAsync(params.id || "");

    if (!response.isSuccess) {
      ToastUtil.showErrorMessage("Failed to download files.");
    }
  };

  useEffect(() => {
    const getContentByIdAsync = async () => {
      const response = await contentService.getByIdAsync(params.id || "");

      if (response.isSuccess) {
        setContent(response.result!);
      }

      setIsLoading(false);
    };

    getContentByIdAsync();
  }, [params.id]);

  useEffect(() => {
    const connection = signalrService.buildConnection();

    connection?.on(SignalRMethodsEnum.LikeCountChanged, (data) => {
      if (data.contentId !== contentRef.current?.id) return;

      setContent((prevContent) => {
        return {
          ...prevContent,
          likeCount: data.count,
        };
      });
    });

    return () => {
      connection?.off(SignalRMethodsEnum.LikeCountChanged);
    };
  }, []);

  return (
    <div className="d-flex flex-column vh-100">
      <div className="flex-grow-1">
        <NavBar />
        <Loader isLoading={isLoading}>
          <div className="container mt-4">
            {content ? (
              <div>
                <div className="d-flex justify-content-between align-items-center">
                  <h1 className="mb-4">{content.title}</h1>
                  <div className="mb-4">
                    <LikeButtonComponent content={content} />
                  </div>
                </div>
                <div className="mb-3 text-muted">
                  <span>
                    <strong>Author:</strong>{" "}
                    {content.user?.fullName || "Anonymous"}
                  </span>{" "}
                  <br />
                  <span>
                    <strong>Subject:</strong>{" "}
                    {content.subjectName || "No Subject"}
                  </span>
                </div>

                <div className="mb-5">
                  <h4>Description</h4>
                  <p>{content.text}</p>
                </div>

                {content.files && content.files.length > 0 && (
                  <div>
                    <hr />
                    <div className="mb-5">
                      <div className="d-flex justify-content-between align-items-center mb-3">
                        <h3>
                          <strong>Files</strong>
                        </h3>
                        <div>
                          <button
                            className="btn btn-primary"
                            onClick={handleDownloadAll}
                          >
                            <i className="fas fa-file-archive"></i> Download All
                            Files
                          </button>
                        </div>
                      </div>
                      <div className="d-flex flex-wrap gap-2">
                        {content.files.map((file, index) => (
                          <a
                            href={file.path}
                            download={file.name}
                            target="_blank"
                            key={index}
                            className="text-decoration-none text-white"
                          >
                            <div
                              className="d-flex align-items-center p-2 rounded shadow-sm bg-primary"
                              style={{
                                width: "250px",
                                maxHeight: "38px",
                              }}
                            >
                              <span
                                className="text-truncate me-auto"
                                style={{ maxWidth: "180px" }}
                                title={file.name}
                              >
                                {file.name}
                              </span>

                              <FaDownload
                                style={{ fontSize: "14px", marginRight: "5px" }}
                              />
                            </div>
                          </a>
                        ))}
                      </div>
                    </div>
                  </div>
                )}
              </div>
            ) : (
              <p className="text-center">Content not found</p>
            )}
          </div>
        </Loader>
      </div>
      <FooterComponent />
    </div>
  );
}
