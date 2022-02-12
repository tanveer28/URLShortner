import React, { useState } from "react";
import { InputGroup } from "react-bootstrap";
import { Button, Form } from "react-bootstrap";

export const AppForm = (): JSX.Element => {
  const [url, setUrl] = useState("");
  const [shortUrl, setShortUrl] = useState("");

  const onClick = () => {
    getData().then((x) => {
      setShortUrl(x);
    });
  };

  const getData = () => {
    return fetch(`https://localhost:7146/Short?url=${url}`, {
      method: "POST",
      mode: "cors",
      headers: { "Content-Type": "application/json" },
    })
      .then((res) => res.text())
      .then((res: string) => {
        return res;
      });
  };

  const onChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setUrl(event.currentTarget.value);
  };

  return (
    <Form>
      <Form.Group className="mb-3" controlId="formBasicEmail">
        <Form.Label>Url to shorten</Form.Label>
        <InputGroup hasValidation>
          <Form.Control
            onChange={onChange}
            type="text"
            placeholder="Enter a URL"
          />
          <Button variant="primary" onClick={onClick}>
            Shorten!
          </Button>
        </InputGroup>
      </Form.Group>

      <Form.Group className="mb-3" controlId="formBasicPassword">
        <Form.Label>Shortened Url</Form.Label>
        <Form.Control
          disabled
          type="text"
          placeholder="Your new URL goes here..."
          value={shortUrl}
        />
      </Form.Group>
    </Form>
  );
};
