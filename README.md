# Bank Debit Order File Parser
Parses a debit order xml file into bank specific flat files.

# Debit Order XML Fields
<pre>
&lt<span class=""fw-bold"">debitorders</span>&gt
  &lt<span class=""fw-bold"">deduction</span>&gt
    &lt<span class=""fw-bold"">accountholder</span>&gtAccount Holder Name&lt<span class=""fw-bold"">/accountholder</span>&gt
	&lt<span class=""fw-bold"">accountnumber</span>&gtAccount Number&lt<span class=""fw-bold"">/accountnumber</span>&gt
	&lt<span class=""fw-bold"">accounttype</span>&gtAccount Type (cheque, savings, credit card, other)&lt<span class=""fw-bold"">/accounttype</span>&gt
	&lt<span class=""fw-bold"">bankname</span>&gtBank Name&lt<span class=""fw-bold"">/bankname</span>&gt
	&lt<span class=""fw-bold"">branch</span>&gtBank Branch&lt<span class=""fw-bold"">/branch</span>&gt
	&lt<span class=""fw-bold"">amount</span>&gtAmount Deducted (decimal value)&lt<span class=""fw-bold"">/amount</span>&gt
	&lt<span class=""fw-bold"">date</span>&gtDate Deducted (MM/dd/yyyy)&lt<span class=""fw-bold"">/date</span>&gt
  &lt<span class=""fw-bold"">/deduction</span>&gt
&lt<span class=""fw-bold"">/debitorders</span>&gt
</pre>

# Getting Started
Use these instructions to get the project up and running.

### Prerequisites
1. [Install DotNet Core 3.1](https://dotnet.microsoft.com/en-us/download/dotnet/3.1)

### Installation
1. Clone the repository
2. At the root directory, restore required packages by running:
<pre>
dotnet restore
</pre>
3. Then build the solution:
<pre>
dotnet build
</pre>
4. After that, launch the backend by:
<pre>
dotnet run
</pre>

Follow the url to view the web app!
