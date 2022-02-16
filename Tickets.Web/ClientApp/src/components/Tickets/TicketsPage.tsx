import React, { useEffect, useState } from "react";
import { Button, Col, Input, Label, Row, Spinner } from "reactstrap";
import { ErrorContainer, TicketsTable } from "..";
import {Employee, Project, ProjectFromJSON, ProjectToJSON, Status, Ticket, TicketFromJSON} from "../../models";
import { useEmployeesProxy, useProjectsProxy, useStatusProxy,useTicketsProxy  } from "../../proxies";
import { toast } from "react-toastify";
import { useParams } from "react-router-dom";
import { TicketsForm } from ".";

export const TicketsPage = () => {
	const {projectId: paramProjectId} = useParams();
	const paramProjectIdNumber = paramProjectId ? parseInt(paramProjectId,10) : 0;

	const [projects, setProjects] = useState<Project[] | null | undefined>(null);
	const [statuses, setStatuses] = useState<Status[] | null | undefined>(null);
	const [employees, setEmployees] = useState<Employee[] | null | undefined>(null);
	const [tickets, setTickets] = useState<Ticket[]| null | undefined>(null);
	const [selectedProjectId, setSelectedProjectId] = useState<number>(0);
	const [selectedTicket, setSelectedTicket] = useState<Ticket | null | undefined>(null);

	const projectProxy = useProjectsProxy();
	const statusProxy = useStatusProxy();
	const ticketProxy = useTicketsProxy();
	const employeesProxy = useEmployeesProxy();

	// Load the list of available projects the first time.
	useEffect(() => {
		if (!projectProxy.working && !projectProxy.error && projects == null) {
			loadProjects();
		}
	},[]);
	
	
	const loadProjects = ()=>{
		projectProxy.getList().subscribe(
			(result: Project[]) => {
			  setProjects(result);
			},
			(error: any) => {
			  setProjects(null);
			}
		);
	}

	// Load the list of available status to pass it to forms the first time.
	useEffect(() => {
		if (!statusProxy.working && !statusProxy.error && statuses == null) {
			loadStatuses();
		}
	},[]);

	const loadStatuses = ()=>{
		statusProxy.getList().subscribe(
			(result: Status[]) => {
			  setStatuses(result);
			},
			(error: any) => {
			  setStatuses(null);
			}
		);
	}

	// Load the list of available employees to pass it to forms the first time.
	useEffect(() => {
		if (!employeesProxy.working && !employeesProxy.error && employees == null) {
			loadEmployees();
		}
	},[]);

	const loadEmployees = ()=>{
		employeesProxy.getList().subscribe(
			(result: Employee[]) => {
				setEmployees(result);
			},
			(error: any) => {
				setEmployees(null);
			}
		);
	}

	//Load Ticket when projectId changes.
	useEffect(() => {
		if(selectedProjectId === 0)
		{
			setTickets(null);
		}
		else{
			if (!ticketProxy.working && !ticketProxy.error) {
				loadTickets(selectedProjectId);
			}
		}
	},[selectedProjectId]);

	const loadTickets = (projectId : number)=>{
		ticketProxy.search(projectId).subscribe(
			(result: Ticket[]) => {
			  setTickets(result);
			},
			(error: any) => {
			  setStatuses(null);
			}
		);
	}

	//If param changed refresh
	useEffect(() => {
		if(paramProjectIdNumber !== 0 && paramProjectIdNumber !== selectedProjectId)	
		{
			setSelectedProjectId(paramProjectIdNumber);
		}
		
	},[paramProjectIdNumber]);

	const handleAddClick = ()=>{
		const newTicket = TicketFromJSON({ticketId:0, projectId: selectedProjectId});
		setSelectedTicket(newTicket);
	}

	const handleSave = (ticket:Ticket)=>{
		if(ticket.employeeId == 0) ticket.employeeId = null; //Remove unasigned Employee.
		if(ticket.ticketId === 0){
			ticketProxy.add(ticket).subscribe(
				(resp: Ticket) => {
					if(tickets)
					{
						setTickets([{ ...resp }, ...tickets]);
						setSelectedTicket(resp);
						toast.success("Ticket " + resp.title + "  added!");
					}
				}
			);
		}
		else{
			ticketProxy.edit(ticket).subscribe(
				(resp: Ticket) => {
					if(tickets)
					{
						const ticketAux = tickets.map((ticket) => (resp && ticket.ticketId == resp.ticketId ? resp : ticket));
						setTickets(ticketAux);
						setSelectedTicket(ticket)
						toast.success("Ticket " + resp.title + "  modified!");
					}
				}
			);
		}
	}

	if (projectProxy.working) return <Spinner />;
	if (statusProxy.working) return <Spinner />;
	if (employeesProxy.working) return <Spinner />;

	return (
		<>
		{projectProxy.error != null && <ErrorContainer error={projectProxy.error} />}
		{ticketProxy.error != null && <ErrorContainer error={ticketProxy.error} />}
		{statusProxy.error != null && <ErrorContainer error={statusProxy.error} />}
		{employeesProxy.error != null && <ErrorContainer error={statusProxy.error} />}
	
		<Row>
			<Col sm={6} xs={12}>
				<div>
					<Row>
						<Col sm={6} xs={12}>
							<Label>Projects</Label>
							<Input
							type="select"
							name="project"
							value={selectedProjectId ? selectedProjectId : 0}
							onChange={(e)=>{
								const projectId = parseInt(e.target.value,10);
								setSelectedProjectId(projectId)
							}}
							>
								<option key={0} value={0}>
											Select Project
										</option>
								{projects &&
									projects.map((project: Project) =>
									project.projectId ? (
										<option key={project.projectId} value={project.projectId}>
											{project.projectName + " - (" + project.status?.statusDesc + ")"}
										</option>
										) : null
								)}
							</Input>
						</Col>
					</Row>
				</div>
				
				{ticketProxy.working && <Spinner/>}
				{selectedProjectId !== 0 && !ticketProxy.working && ! ticketProxy.error && 
				<>
					<div className="d-flex flex-row-reverse">
						<Button color="primary" style={{marginBottom:'5px'}} onClick={handleAddClick}>
							Add
						</Button>{" "}
					</div>
					<TicketsTable tickets={tickets} selectedTicket={selectedTicket} setSelectedTicket={setSelectedTicket}/>
				</>
				}
			</Col>
			<Col sm={6} xs={12}>
				{selectedTicket && <TicketsForm ticket={selectedTicket} handleSave={handleSave} statuses={statuses} employees={employees}/>
				}
			</Col>
		</Row>
	
		</>
	)

};



