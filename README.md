# Outbox

Outbox is simple library to support Outbox pattern in your system.

## Table of contents
* [Introduction](#Introduction)
* [Basics](#Basics)
* [Retry policies](#Retry-policies)
* [Persistence](#Persistence)
* [Sources](#Sources)

## Introduction

Sometimes our systems need to communicate with external components like external service or mail server for example to sending email after placing an order. Unfortunately external component can be unavailable at moment processing a business operation. Outbox Pattern helps us provide atomicity of our business operation.

## Basics

TODO

## Retry policies

TODO

### Poison queue

## Persistence

TODO

### Postgres

### MongoDB

### Redis

## Sources

https://www.kamilgrzybek.com/blog/posts/the-outbox-pattern
