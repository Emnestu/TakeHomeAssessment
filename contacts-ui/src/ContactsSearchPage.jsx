import './ContactsSearchPage.css';
import React, { useState, useEffect } from "react";

export default function ContactSearch() {
  const baseUrl = "http://localhost:5272";
  const pageSize = 10;

  const [contacts, setContacts] = useState([]);
  const [offices, setOffices] = useState([]);
  const [searchTerm, setSearchTerm] = useState("");
  const [selectedOfficeId, setSelectedOfficeId] = useState(null);
  const [page, setPage] = useState(1);
  const [totalRecords, setTotalRecords] = useState(0);
  
  const totalPages = Math.ceil(totalRecords / pageSize);

  useEffect(() => {
    fetch(`${baseUrl}/api/v1/Offices/GetOfficesWithContacts`)
      .then((res) => res.json())
      .then((data) => setOffices(data.data));
  }, []);

  const fetchContacts = (pageNumber) => {
    const params = new URLSearchParams({
      searchTerm: searchTerm,
      page: pageNumber.toString(),
      pageSize: pageSize.toString(),
    });
    if (selectedOfficeId) {
      params.append("officeId", selectedOfficeId);
    }

    fetch(`${baseUrl}/api/v1/Contacts/GetContacts?${params.toString()}`)
      .then(res => res.json())
      .then(data => {
        setContacts(data.data);
        setTotalRecords(data.totalRecords);
      });
    };

  const onSearchClick = () => {
    const firstPage = 1;
    setPage(firstPage);
    fetchContacts(firstPage);
  };

  const onPrevPage = () => {
    if (page > 1) {
      const newPage = page - 1;
      setPage(newPage);
      fetchContacts(newPage);
    }
  };

  const onNextPage = () => {
    const newPage = page + 1;
    setPage(newPage);
    fetchContacts(newPage);
  };

  return (
    <div className="container">
      <div className="search-controls">
        <input
          type="text"
          placeholder="Name or Email"
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          className="search-input"
        />
        <select
          value={selectedOfficeId || ""}
          onChange={e => setSelectedOfficeId(e.target.value || null)}
          className="office-select"
        >
          <option value="">{"<All>"}</option>
          {offices.map(o => (
            <option key={o.officeId} value={o.officeId}>
              {o.name}
            </option>
          ))}
        </select>
        <button onClick={onSearchClick} className="search-button">
          Search
        </button>
      </div>

      <table className="contacts-table">
        <thead>
          <tr>
            <th>Name</th>
            <th>Email</th>
            <th>Office</th>
          </tr>
        </thead>
        <tbody>
          {contacts.length === 0 ? (
            <tr>
              <td colSpan="3" className="no-results">
                No results found.
              </td>
            </tr>
          ) : (
            contacts.map((c, idx) => (
              <tr key={idx}>
                <td>{c.name}</td>
                <td>{c.email}</td>
                <td>{c.officeNames?.join(", ")}</td>
              </tr>
            ))
          )}
        </tbody>
      </table>

      <div className="pagination">
        <button onClick={onPrevPage} disabled={page === 1} className="page-button">
          Previous
        </button>
        <button onClick={onNextPage} disabled={page >= totalPages} className="page-button">
          Next
        </button>
      </div>
    </div>
  );
}